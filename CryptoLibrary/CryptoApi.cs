using Newtonsoft.Json;

namespace CryptoLibrary
{
    public class CryptoApi
    {
        private readonly HttpClient _httpClient = new HttpClient();
    
        private const string _baseUrl = "https://api.coinlayer.com";

        public async Task<Dictionary<string, decimal>> GetCryptoRatesAsync(List<string> symbols, string currency = "usd")
        {
            var symbolString = string.Join(",", symbols);
            var url = $"{_baseUrl}?ids={symbolString}&vs_currencies={currency}";

            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, decimal>>>(response);

            var rates = new Dictionary<string, decimal>();
            foreach (var item in data)
            {
                rates[item.Key] = item.Value[currency];
            }
            return rates;
     
        }

        public async Task<Dictionary<string, decimal>> FetchCurrentCryptoPrices()
        {
            string endpoint = _baseUrl + "/live?access_key=058e86578b8879aa9cbe3ac580f93e8b";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    // Deserialize the JSON response
                    var cryptoResponse = JsonConvert.DeserializeObject<CryptoResponse>(jsonResponse);
                    if (cryptoResponse != null && cryptoResponse.Rates != null)
                    {
                        return cryptoResponse.Rates;
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Deserialization error: {ex.Message}");
                }
            }

            // Return an empty dictionary if response fails or data is unavailable
            return new Dictionary<string, decimal>();
        }



    }
}
