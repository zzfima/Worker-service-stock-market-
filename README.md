# Stock Market Monitor Application

A WPF (Windows Presentation Foundation) application that sits in the system tray and monitors stock prices with periodic popup notifications.

## Features

- **System Tray Integration**: Application runs in the system tray and can only be closed from the tray menu
- **Real Stock Data**: Fetches live stock prices from Yahoo Finance API (no API key required)
- **Stock Watching**: Add/remove stocks to monitor via the configuration window
- **Periodic Updates**: Configurable update interval (default: 5 minutes)
- **Popup Notifications**: Shows stock prices in a movable, always-on-top popup window
- **Configuration**: Customize update interval, popup behavior, and watched stocks
- **Persistence**: Settings and watched stocks are saved automatically

## Usage

1. **Run the Application**: Execute `WorkerServiceStockMarket.exe` from the `bin\Debug\net10.0-windows` folder
2. **System Tray Icon**: The app will appear in the system tray with the stock-market icon
3. **Configure Stocks**: Right-click the tray icon and select "Configure" to add/remove stocks
4. **View Prices**: Click "Show Stock Prices" or "Update Now" to see current prices
5. **Exit**: Only accessible via the "Exit" option in the system tray menu

## Configuration Options

- **Update Interval**: Set how often to fetch stock prices (1-60 minutes)
- **Show Popup**: Enable/disable popup notifications on updates
- **Always on Top**: Keep the popup window above other windows
- **Popup Duration**: How long the popup stays visible (5-300 seconds)

## Stock Data

The application uses Yahoo Finance's free API to fetch real-time stock prices. No API key is required. Supported stock symbols include:
- US stocks: AAPL, GOOGL, MSFT, TSLA, etc.
- International stocks: Most major stock symbols supported by Yahoo Finance

If Yahoo Finance is unavailable, the application falls back to consistent mock data (same price for the same symbol).

## Configuration File

Settings are stored in:
`%AppData%\StockMarketApp\stockmarket_config.json`

## Building

```bash
dotnet build
```

## Running

```bash
dotnet run
```

Or run the executable directly:
`bin\Debug\net10.0-windows\WorkerServiceStockMarket.exe`

## Technical Details

- **Framework**: .NET 10.0 WPF
- **Architecture**: MVVM-like separation with Models, Services, and Views
- **Dependencies**: 
  - Newtonsoft.Json for JSON handling
  - Hardcodet.NotifyIcon.Wpf for system tray integration
- **Persistence**: JSON file in AppData folder
- **Stock Data Source**: Yahoo Finance API (free, no API key required)
