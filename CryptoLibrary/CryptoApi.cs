using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CryptoLibrary
{
    public class Crypto
{
    public string Symbol{get; set;}
    


}

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

        public async Task FetchCurrentCryptoPrices() {
            string endpoint = _baseUrl + "/live?access_key=058e86578b8879aa9cbe3ac580f93e8b";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            if(response.IsSuccessStatusCode){
                Crypto crypto = new Crypto();
                
            }
        }

    }
}
