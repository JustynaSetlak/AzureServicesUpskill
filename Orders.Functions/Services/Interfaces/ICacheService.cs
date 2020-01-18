namespace Orders.Functions.Services.Interfaces
{
    public interface ICacheService
    {
        void SetValueToCache(string key, string value);
    }
}
