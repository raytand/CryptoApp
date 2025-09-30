using CryptoApp.Models;

namespace CryptoApp.Services
{
    public interface ICoinService
    {
        Task<List<Coin>> GetTopCoinsAsync();

        Task<CoinDetail> GetCoinDetailAsync(string id);

        Task<List<MarketInfo>> GetCoinMarketsAsync(string id);
    }
}
