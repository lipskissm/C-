using Newtonsoft.Json;

public class CryptoResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("rates")]
    public Dictionary<string, decimal> Rates { get; set; }
}