
using Microsoft.AspNetCore.Http;

namespace ZikoraService.Application.Services
{
    public interface ITokenService
    {
        string Token { get; }
        string RefreshToken { get; }
        bool HasValidTokens { get; }
    }

    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Token => GetHeaderValue("token");
        public string RefreshToken => GetHeaderValue("refreshToken");
        public bool HasValidTokens => !string.IsNullOrEmpty(Token) && !string.IsNullOrEmpty(RefreshToken);

        private string GetHeaderValue(string headerName)
        {
            if (_httpContextAccessor?.HttpContext?.Request?.Headers != null)
            {
                // Handle case-insensitive header names
                var header = _httpContextAccessor.HttpContext.Request.Headers
                    .FirstOrDefault(h => string.Equals(h.Key, headerName, StringComparison.OrdinalIgnoreCase));

                return header.Value.ToString();
            }
            return string.Empty;
        }
    }
}

