using System.Text.Json.Serialization;

public class Coin
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    [JsonPropertyName("current_price")]
    public double CurrentPrice { get; set; }

    [JsonPropertyName("price_change_percentage_24h")]
    public double? PricePercentage24h { get; set; }
}
