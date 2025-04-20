namespace ZikoraService.Infrastructure.Http
{
    public interface IRemoteHttpClient
    {
        Task<T> PostJSON<T>(string endpoint, object payload = null, object headers = null, object cookies = null);

    }
}
