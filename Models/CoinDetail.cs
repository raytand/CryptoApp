namespace CryptoApp.Models
{
    public class CoinDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double CurrentPrice { get; set; }
        public double MarketCap { get; set; }
        public double PriceChangePercentage24h { get; set; }
        public string Homepage { get; set; }
    }

    public class MarketInfo
    {
        public string Exchange { get; set; }
        public double Price { get; set; }
        public string TradeUrl { get; set; }
    }

}
