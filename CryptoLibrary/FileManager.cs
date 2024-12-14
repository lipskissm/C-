using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CryptoLibrary
{
    public class FileManager
    {
        public void SaveToJson(string filePath, Dictionary<string, decimal> newData)
        {
            Dictionary<string, List<decimal>> existingData;

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                try
                {
                    existingData = JsonConvert.DeserializeObject<Dictionary<string, List<decimal>>>(json) ?? new Dictionary<string, List<decimal>>();
                }
                catch (JsonSerializationException)
                {
                    existingData = new Dictionary<string, List<decimal>>();
                }
            }
            else
            {
                existingData = new Dictionary<string, List<decimal>>();
            }

            foreach (var entry in newData)
            {
                if (!existingData.ContainsKey(entry.Key))
                {
                    existingData[entry.Key] = new List<decimal>();
                }
                existingData[entry.Key].Add(entry.Value);
            }

            var updatedJson = JsonConvert.SerializeObject(existingData, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
        }

        public Dictionary<string, List<decimal>> LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.");

            var json = File.ReadAllText(filePath);
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, List<decimal>>>(json) ?? new Dictionary<string, List<decimal>>();
            }
            catch (JsonSerializationException)
            {
                return new Dictionary<string, List<decimal>>();
            }
        }
    }
}
