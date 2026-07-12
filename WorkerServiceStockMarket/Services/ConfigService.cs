using System.IO;
using System.Text.Json;
using WorkerServiceStockMarket.Models;

namespace WorkerServiceStockMarket.Services
{
    public class ConfigService
    {
        private const string ConfigFileName = "stockmarket_config.json";
        private readonly string _configPath;

        public ConfigService()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appFolder = Path.Combine(appDataPath, "StockMarketApp");
            Directory.CreateDirectory(appFolder);
            _configPath = Path.Combine(appFolder, ConfigFileName);
        }

        public AppConfig LoadConfig()
        {
            if (File.Exists(_configPath))
            {
                try
                {
                    var json = File.ReadAllText(_configPath);
                    return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
                }
                catch
                {
                    return new AppConfig();
                }
            }
            return new AppConfig();
        }

        public void SaveConfig(AppConfig config)
        {
            var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configPath, json);
        }
    }
}
