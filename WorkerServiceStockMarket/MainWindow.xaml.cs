using System;
using System.Windows;
using WorkerServiceStockMarket.Models;
using WorkerServiceStockMarket.Services;

namespace WorkerServiceStockMarket
{
    public partial class MainWindow : Window
    {
        private readonly StockService _stockService;
        private readonly ConfigService _configService;
        private AppConfig _config;
        private System.Windows.Threading.DispatcherTimer? _updateTimer;
        private StockPopupWindow? _popupWindow;

        public MainWindow()
        {
            InitializeComponent();
            _stockService = new StockService();
            _configService = new ConfigService();
            _config = _configService.LoadConfig();
            
            InitializeTimer();
            this.StateChanged += (sender, e) =>
            {
                if (this.WindowState == WindowState.Minimized)
                {
                    this.Hide();
                }
            };
        }

        private void InitializeTimer()
        {
            _updateTimer = new System.Windows.Threading.DispatcherTimer();
            _updateTimer.Interval = TimeSpan.FromMinutes(_config.UpdateIntervalMinutes);
            _updateTimer.Tick += async (sender, e) => await UpdateStockPrices();
            _updateTimer.Start();
        }

        private async void ShowPrices_Click(object sender, RoutedEventArgs e)
        {
            await UpdateStockPrices();
        }

        private void Configure_Click(object sender, RoutedEventArgs e)
        {
            var configWindow = new ConfigurationWindow(_config);
            if (configWindow.ShowDialog() == true)
            {
                _config = configWindow.Config;
                _configService.SaveConfig(_config);
                
                _updateTimer.Interval = TimeSpan.FromMinutes(_config.UpdateIntervalMinutes);
                _updateTimer.Stop();
                _updateTimer.Start();
            }
        }

        private async void UpdateNow_Click(object sender, RoutedEventArgs e)
        {
            await UpdateStockPrices();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NotifyIcon.Dispose();
            Application.Current.Shutdown();
        }

        private async System.Threading.Tasks.Task UpdateStockPrices()
        {
            if (_config.WatchedStocks.Count == 0)
                return;

            try
            {
                var stocks = await _stockService.GetMultipleStockPricesAsync(_config.WatchedStocks);
                
                if (stocks.Count > 0 && _config.ShowPopup)
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        if (_popupWindow == null || !_popupWindow.IsLoaded)
                        {
                            _popupWindow = new StockPopupWindow(stocks, _config);
                        }
                        else
                        {
                            _popupWindow.UpdateStocks(stocks);
                        }
                        
                        _popupWindow.Topmost = _config.AlwaysOnTop;
                        _popupWindow.Show();
                        _popupWindow.Activate();
                    });
                }

                NotifyIcon.ToolTipText = $"Stocks: {stocks.Count} | Last: {DateTime.Now:HH:mm}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating stock prices: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            base.OnClosing(e);
        }
    }
}
