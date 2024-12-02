using System.IO;
using Newtonsoft.Json;

namespace CryptoLibrary
{
    public class FileManager
    {
        public void SaveToJson<T>(string filePath, T data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public T LoadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.");

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
