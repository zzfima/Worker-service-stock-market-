# Stock Market Monitor Application

A WPF (Windows Presentation Foundation) application that sits in the system tray and monitors stock prices with periodic popup notifications.

## Features

- **System Tray Integration**: Application runs in the system tray and can only be closed from the tray menu
- **Stock Watching**: Add/remove stocks to monitor via the configuration window
- **Periodic Updates**: Configurable update interval (default: 5 minutes)
- **Popup Notifications**: Shows stock prices in a movable, always-on-top popup window
- **Configuration**: Customize update interval, popup behavior, and watched stocks
- **Persistence**: Settings and watched stocks are saved automatically

## Usage

1. **Run the Application**: Execute `WorkerServiceStockMarket.exe` from the `bin\Debug\net10.0-windows` folder
2. **System Tray Icon**: The app will appear in the system tray with an application icon
3. **Configure Stocks**: Right-click the tray icon and select "Configure" to add/remove stocks
4. **View Prices**: Click "Show Stock Prices" or "Update Now" to see current prices
5. **Exit**: Only accessible via the "Exit" option in the system tray menu

## Configuration Options

- **Update Interval**: Set how often to fetch stock prices (1-60 minutes)
- **Show Popup**: Enable/disable popup notifications on updates
- **Always on Top**: Keep the popup window above other windows
- **Popup Duration**: How long the popup stays visible (5-300 seconds)

## Stock Data

The application currently uses mock data for demonstration purposes. To use real stock data:

1. Get a free API key from [Alpha Vantage](https://www.alphavantage.co/support/#api-key)
2. Update the `AlphaVantageApiKey` constant in `Services/StockService.cs`
3. Replace `YOUR_API_KEY` with your actual API key

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

- **Framework**: .NET 10.0 Windows Forms
- **Architecture**: MVVM-like separation with Models, Services, and Forms
- **Dependencies**: Newtonsoft.Json for JSON handling
- **Persistence**: JSON file in AppData folder
