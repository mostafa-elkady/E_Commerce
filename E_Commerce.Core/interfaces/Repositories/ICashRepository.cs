namespace E_Commerce.Core.interfaces.Repositories
{
    public interface ICashRepository
    {
        Task SetCashResponseAsync(string key, object response, TimeSpan time);
        Task<string?> GetCashResponseAsync(string key);
    }
}
