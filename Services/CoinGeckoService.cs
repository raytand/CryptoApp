using CryptoApp.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace CryptoApp.Services
{
    public class CoinGeckoService : ICoinService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _topCoinsEndpoint;
        public CoinGeckoService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("CoinGecko");
            _apiKey = configuration["CoinGeckoService:API_KEY"];
            _topCoinsEndpoint = configuration["CoinGeckoService:API_GET_COINS_ENDPOINT"];
        }

        public async Task<List<Coin>> GetTopCoinsAsync()
        {
            var url = _topCoinsEndpoint + _apiKey;

            var json = await _httpClient.GetStringAsync(url);

            return JsonSerializer.Deserialize<List<Coin>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<CoinDetail> GetCoinDetailAsync(string id)
        {
            var url = $"coins/{id}?localization=false&tickers=false&market_data=true&x_cg_demo_api_key={_apiKey}";
            var json = await _httpClient.GetStringAsync(url);
            var doc = JsonDocument.Parse(json);
            var data = doc.RootElement;
            var market = data.GetProperty("market_data");

            return new CoinDetail
            {
                Id = data.GetProperty("id").GetString(),
                Name = data.GetProperty("name").GetString(),
                Symbol = data.GetProperty("symbol").GetString(),
                CurrentPrice = market.GetProperty("current_price").GetProperty("usd").GetDouble(),
                MarketCap = market.GetProperty("market_cap").GetProperty("usd").GetDouble(),
                PriceChangePercentage24h = market.GetProperty("price_change_percentage_24h").GetDouble(),
                Homepage = data.GetProperty("links").GetProperty("homepage")[0].GetString()
            };

        }
        
        public async Task<List<MarketInfo>> GetCoinMarketsAsync(string id)
        {
            var url = $"coins/{id}/tickers? x_cg_demo_api_key={_apiKey}";
            var json = await _httpClient.GetStringAsync(url);

            var doc = JsonDocument.Parse(json);
            var markets = new List<MarketInfo>();

            foreach (var ticker in doc.RootElement.GetProperty("tickers").EnumerateArray())
            {
                markets.Add(new MarketInfo
                {
                    Exchange = ticker.GetProperty("market").GetProperty("name").GetString(),
                    Price = ticker.GetProperty("last").GetDouble(),
                    TradeUrl = ticker.GetProperty("trade_url").GetString()
                });
            }
            return markets;
        }
    }
}
