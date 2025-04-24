namespace ZikoraService.Infrastructure.Http
{
    public interface IRemoteHttpClient
    {
        Task<T> PostJSON<T>(string endpoint, object payload = null, object headers = null, object cookies = null);
        Task<T> Post<T>(string url, object data, Dictionary<string, string> headers = null);
    }
}
