using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WorkerServiceStockMarket.Models;

namespace WorkerServiceStockMarket
{
    public partial class ConfigurationWindow : Window
    {
        private AppConfig _config;

        public AppConfig Config => _config;

        public ConfigurationWindow(AppConfig config)
        {
            InitializeComponent();
            _config = config;
            LoadConfig();
        }

        private void LoadConfig()
        {
            lstStocks.Items.Clear();
            foreach (var stock in _config.WatchedStocks)
            {
                lstStocks.Items.Add(stock);
            }

            numInterval.Text = _config.UpdateIntervalMinutes.ToString();
            chkShowPopup.IsChecked = _config.ShowPopup;
            chkAlwaysOnTop.IsChecked = _config.AlwaysOnTop;
            numPopupDuration.Text = _config.PopupDurationSeconds.ToString();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var symbol = txtStockSymbol.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(symbol) && !lstStocks.Items.Contains(symbol))
            {
                lstStocks.Items.Add(symbol);
                txtStockSymbol.Clear();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var itemsToRemove = lstStocks.SelectedItems.Cast<string>().ToList();
            foreach (var item in itemsToRemove)
            {
                lstStocks.Items.Remove(item);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _config.WatchedStocks.Clear();
            foreach (var item in lstStocks.Items)
            {
                _config.WatchedStocks.Add(item.ToString()!);
            }

            if (int.TryParse(numInterval.Text, out int interval))
            {
                _config.UpdateIntervalMinutes = interval;
            }
            _config.ShowPopup = chkShowPopup.IsChecked ?? true;
            _config.AlwaysOnTop = chkAlwaysOnTop.IsChecked ?? true;
            
            if (int.TryParse(numPopupDuration.Text, out int duration))
            {
                _config.PopupDurationSeconds = duration;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
