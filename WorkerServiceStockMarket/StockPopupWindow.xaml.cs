using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WorkerServiceStockMarket.Models;

namespace WorkerServiceStockMarket
{
    public partial class StockPopupWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer? _autoCloseTimer;
        private int _durationSeconds;

        public StockPopupWindow(System.Collections.Generic.List<Stock> stocks, AppConfig config)
        {
            InitializeComponent();
            _durationSeconds = config.PopupDurationSeconds;
            UpdateStocks(stocks);
            
            this.Loaded += (sender, e) => PositionWindow();
        }

        private void PositionWindow()
        {
            var workingArea = SystemParameters.WorkArea;
            this.Left = workingArea.Right - this.Width - 20;
            this.Top = workingArea.Bottom - this.Height - 20;
        }

        public void UpdateStocks(System.Collections.Generic.List<Stock> stocks)
        {
            var stockViewModels = stocks
                .OrderByDescending(s => s.ChangePercent)
                .Select(s => new StockViewModel(s))
                .ToList();
            
            StocksPanel.ItemsSource = stockViewModels;
            
            _autoCloseTimer?.Stop();
            _autoCloseTimer = new System.Windows.Threading.DispatcherTimer();
            _autoCloseTimer.Interval = TimeSpan.FromSeconds(_durationSeconds);
            _autoCloseTimer.Tick += (sender, e) =>
            {
                _autoCloseTimer.Stop();
                this.Hide();
            };
            _autoCloseTimer.Start();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            _autoCloseTimer?.Stop();
            base.OnClosing(e);
        }
    }

    public class StockViewModel
    {
        public StockViewModel(Stock stock)
        {
            Symbol = stock.Symbol;
            Price = stock.Price;
            ChangeText = $"{stock.Change:+0.00;-0.00} ({stock.ChangePercent:+0.00;-0.00}%)";
            ChangeColor = stock.ChangePercent >= 0 ? Brushes.DarkGreen : Brushes.DarkRed;
            BackgroundColor = stock.ChangePercent >= 0 ? Brushes.LightGreen : Brushes.LightPink;
            LastUpdate = stock.LastUpdate;
        }

        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public string ChangeText { get; set; }
        public Brush ChangeColor { get; set; }
        public Brush BackgroundColor { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
