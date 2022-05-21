namespace AirPurity.API.BusinessLogic.External.Interfaces
{
    public interface IHttpClientContext
    {
        Task<T> Get<T>(string uri);
        Task<T> Get<T>(string uri, object param);
    }
}
