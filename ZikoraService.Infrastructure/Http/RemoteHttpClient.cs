using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly.CircuitBreaker;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using ZikoraService.Infrastructure.Extensions;

namespace ZikoraService.Infrastructure.Http
{
    public class RemoteHttpClient : IRemoteHttpClient
    {
        private readonly FlurlClient flurlClient;
        private readonly ILogger<RemoteHttpClient> _logger;
        private readonly CircuitBreakerStateService _circuitState;
        private readonly ExternalApiSettings _apiSettings;
        private readonly int TimeOut = 120;

        public RemoteHttpClient(
    ILogger<RemoteHttpClient> logger,
    CircuitBreakerStateService circuitState,
    LoggingHandler loggingHandler,
    IOptions<ExternalApiSettings> apiSettings)
        {
            _logger = logger;
            _circuitState = circuitState;
            _apiSettings = apiSettings.Value;

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            // Chain handlers
            loggingHandler.InnerHandler = httpClientHandler;

            HttpClient httpClient = new HttpClient(loggingHandler)
            {
                BaseAddress = new Uri(_apiSettings.BaseUrl),
                Timeout = TimeSpan.FromSeconds(_apiSettings.TimeoutInSeconds)
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("X-Request-Source", "ZikoraService");

            flurlClient = new FlurlClient(httpClient);
        }
        public async Task<T> PostJSON<T>(string path, object payload = null, object headers = null, object cookies = null)
        {
            try
            {
                var request = flurlClient.Request(path)
                                         .WithTimeout(TimeOut)
                                         .AllowHttpStatus("200,400,404")
                                         .WithCookies(cookies ?? new { })
                                         .WithHeaders(headers ?? new { });
                var response = await request.PostJsonAsync(payload ?? new object());
                _logger.LogInformation("Using Flurl to call API endpoint with values {@endpoint}|{@request}|{@response}", path, payload, response);
                var responseReturned = await response.GetJsonAsync<T>();
                _logger.LogInformation("returned", responseReturned);
                return responseReturned;
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError(ex, "Error Occurred Flurl {@request}", payload);
                if (ex.Call.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.InternalServerError || ex.Call.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw;
                }
                var response = await ex.GetResponseJsonAsync<T>();
                return response;
            }
            catch (TaskCanceledException)
            {
                _logger.LogError("Task was cancelled.");
                return default(T);
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "IOException occurred.");
                return default(T);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }

    

    }

    

}