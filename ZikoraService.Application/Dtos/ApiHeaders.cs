using Microsoft.AspNetCore.Http;

namespace ZikoraService.Application.Dtos
{
    public static class ApiHeaders
    {
        public class AuthHeadersDto
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
        }

        public static Dictionary<string, string> FromPostman(IHeaderDictionary postmanHeaders)
        {
            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["Content-Type"] = "application/json"
            };

            // Copy only relevant headers
            if (postmanHeaders.TryGetValue("token", out var token))
                headers["token"] = token;

            if (postmanHeaders.TryGetValue("refreshToken", out var refreshToken))
                headers["refreshToken"] = refreshToken;

            return headers;
        }
    }
}