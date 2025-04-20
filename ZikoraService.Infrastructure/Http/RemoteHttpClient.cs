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
    public class ExternalApiSettings
    {
        public string BaseUrl { get; set; }
        public int TimeoutInSeconds { get; set; }
        public int MaxRetryAttempts { get; set; }
        public int RetryDelayInSeconds { get; set; }
    }
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
        //    public async Task<string> PostAsync(string endpoint, object payload)
        //    {
        //        try
        //        {
        //            var jsonPayload = JsonConvert.SerializeObject(payload);
        //            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        //            // Get the full HttpResponseMessage
        //            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

        //            // Read the response content
        //            string responseContent = await response.Content.ReadAsStringAsync();

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                throw new HttpRequestException(
        //                    $"API request failed with status code {response.StatusCode}. Response: {responseContent}");
        //            }

        //            return responseContent;
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Failed to execute POST request to {Endpoint}", endpoint);
        //            throw;
        //        }
        //    }
        //}

        private class DelegatingHandlerChain : DelegatingHandler
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
      
        public class ServiceUnavailableException : Exception
        {
            public ServiceUnavailableException() : base() { }
            public ServiceUnavailableException(string message) : base(message) { }
            public ServiceUnavailableException(string message, Exception inner) : base(message, inner) { }
        }
   
        //public class CircuitBreakerStateService
        //{
        //    public bool IsCircuitBroken { get; private set; }
        //    public DateTime? LastBreakTime { get; private set; }
        //    public int FailureCount { get; private set; }

        //    public void OnBreak()
        //    {
        //        IsCircuitBroken = true;
        //        LastBreakTime = DateTime.UtcNow;
        //        FailureCount++;
        //    }

        //    public void OnReset()
        //    {
        //        IsCircuitBroken = false;
        //        LastBreakTime = null;
        //        FailureCount = 0;
        //    }
        // }
    }
}