namespace WorkerServiceStockMarket.Models
{
    public class AppConfig
    {
        public List<string> WatchedStocks { get; set; } = new();
        public int UpdateIntervalMinutes { get; set; } = 5;
        public bool ShowPopup { get; set; } = true;
        public bool AlwaysOnTop { get; set; } = true;
        public int PopupDurationSeconds { get; set; } = 10;
    }
}
