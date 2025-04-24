using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using ZikoraService.Infrastructure.Http;
using ZikoraService.Infrastructure.Persistence.DbContext;
using ZikoraService.Infrastructure.Persistence.Repositories;
using ZikoraService.Infrastructure.Persistence.UnitOfWork;

namespace ZikoraService.Infrastructure.Extensions
{
  
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure PostgreSQL
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => npgsqlOptions
                        .EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null)));

            // Register services
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<CircuitBreakerStateService>();


            // Configure API settings
            services.Configure<ExternalApiSettings>(configuration.GetSection("ExternalApi"));

         services.AddTransient<LoggingHandler>();
            services.AddTransient<IRemoteHttpClient>(provider =>
            {
                return new RemoteHttpClient(
                    provider.GetRequiredService<ILogger<RemoteHttpClient>>(),
                    provider.GetRequiredService<CircuitBreakerStateService>(),
                    provider.GetRequiredService<LoggingHandler>(),
            provider.GetRequiredService<IOptions<ExternalApiSettings>>()
                );
            });

            return services;
        }
        #region helpers
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.InternalServerError)
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(
                    sleepDurations: new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(5)
                    },
                    onRetry: (outcome, delay, retryAttempt, context) =>
                    {
                        context.GetLogger()?.LogWarning(
                            "Retry {RetryAttempt} in {Delay}s for {Uri} due to {Error}",
                            retryAttempt,
                            delay.TotalSeconds,
                            outcome.Result?.RequestMessage?.RequestUri,
                            outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString());
                    });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(CircuitBreakerStateService stateService, ILogger logger)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<HttpRequestException>()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(15),
                    onBreak: (outcome, breakDelay, context) =>
                    {
                        stateService.OnBreak();
                        context.GetLogger()?.LogError(
                            "Circuit breaker opened for {Duration}s due to {Error}",
                            breakDelay.TotalSeconds,
                            outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString());
                    },
                    onReset: (context) =>
                    {
                        stateService.OnReset();
                        context.GetLogger()?.LogInformation("Circuit breaker reset");
                    },
                    onHalfOpen: () =>
                    {
                        logger.LogInformation("Circuit breaker half-open");
                    });
        }
        #endregion
    }
    #region classhelpers
    public static class PollyContextExtensions
    {
        private const string LoggerKey = "ILogger";

        public static Context WithLogger(this Context context, ILogger logger)
        {
            context[LoggerKey] = logger;
            return context;
        }

        public static ILogger GetLogger(this Context context)
        {
            return context.TryGetValue(LoggerKey, out var logger) ? logger as ILogger : null;
        }
    }

    public class CircuitBreakerStateService
    {
        public bool IsCircuitBroken { get; private set; }
        public DateTime? LastBreakTime { get; private set; }

        public void OnBreak()
        {
            IsCircuitBroken = true;
            LastBreakTime = DateTime.UtcNow;
        }

        public void OnReset()
        {
            IsCircuitBroken = false;
            LastBreakTime = null;
        }
    }
    #endregion
}