using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ZikoraService.Infrastructure.Http
{
    public class ExternalApiSettings
    {
        public string BaseUrl { get; set; }
        public int TimeoutInSeconds { get; set; }
        public int MaxRetryAttempts { get; set; }
        public int RetryDelayInSeconds { get; set; }
    }


    #region helpers
    internal class Helpers
    {
    }
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingHandler> _logger;

        public LoggingHandler(ILogger<LoggingHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Sending request to {Method} {Uri}",
                    request.Method, request.RequestUri);

                if (request.Content != null)
                {
                    var requestBody = await request.Content.ReadAsStringAsync();
                    _logger.LogDebug("Request body: {RequestBody}", requestBody);
                }

                var response = await base.SendAsync(request, cancellationToken);

                _logger.LogInformation("Received response {StatusCode} from {Uri}",
                    response.StatusCode, request.RequestUri);

                if (!response.IsSuccessStatusCode && response.Content != null)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error response body: {ResponseBody}", responseBody);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send request to {Uri}", request.RequestUri);
                throw;
            }
        }
    }


    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException() : base() { }
        public ServiceUnavailableException(string message) : base(message) { }
        public ServiceUnavailableException(string message, Exception inner) : base(message, inner) { }
    }
    internal class DelegatingHandlerChain : DelegatingHandler
    {
        public DelegatingHandlerChain(params DelegatingHandler[] handlers)
        {
            for (int i = 0; i < handlers.Length - 1; i++)
            {
                handlers[i].InnerHandler = handlers[i + 1];
            }
            InnerHandler = handlers.LastOrDefault()?.InnerHandler ?? new HttpClientHandler();
        }
    }
    #endregion
}
