using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CryptoLibrary
{
    // A helper class to represent date and price
    public class CryptoEntry
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }

    public class FileManager
    {
        // Save new data to JSON
        public void SaveToJson(string filePath, Dictionary<string, decimal> newData, DateTime selectedDate)
        {
            Dictionary<string, List<CryptoEntry>> existingData;

            // Load existing data if the file exists
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                try
                {
                    existingData = JsonConvert.DeserializeObject<Dictionary<string, List<CryptoEntry>>>(json)
                                   ?? new Dictionary<string, List<CryptoEntry>>();
                }
                catch (JsonSerializationException)
                {
                    existingData = new Dictionary<string, List<CryptoEntry>>();
                }
            }
            else
            {
                existingData = new Dictionary<string, List<CryptoEntry>>();
            }

            // Add new data to existing data
            foreach (var entry in newData)
            {
                if (!existingData.ContainsKey(entry.Key))
                {
                    existingData[entry.Key] = new List<CryptoEntry>();
                }

                // Add the new date-price pair
                existingData[entry.Key].Add(new CryptoEntry
                {
                    Date = selectedDate.ToString("yyyy-MM-dd"),
                    Price = entry.Value
                });
            }

            // Save the updated data back to the file
            var updatedJson = JsonConvert.SerializeObject(existingData, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
        }

        // Load data from JSON
        public Dictionary<string, List<CryptoEntry>> LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.");

            var json = File.ReadAllText(filePath);
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, List<CryptoEntry>>>(json)
                       ?? new Dictionary<string, List<CryptoEntry>>();
            }
            catch (JsonSerializationException)
            {
                return new Dictionary<string, List<CryptoEntry>>();
            }
        }
    }
}
