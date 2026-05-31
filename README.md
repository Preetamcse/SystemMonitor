# SystemMonitor — C# Console Application

SystemMonitor is a lightweight Windows console application built with C# and .NET 8. It continuously monitors your system's CPU usage, RAM consumption, and Disk space at a configurable time interval and outputs the results in real time. The project demonstrates clean architecture using interfaces, dependency injection, and a plugin-based design — making it easy to extend with new monitoring outputs. It currently supports two plugins: one that saves metrics to a local log file, and one that sends the data to an external API via HTTP POST.

---

## Project Structure

```
SystemMonitor/
├── Interfaces/
│   ├── IMonitorPlugin.cs           # Plugin contract
│   └── ISystemMetricsCollector.cs  # Collector contract
├── Models/
│   └── SystemMetrics.cs            # Data model
├── Plugins/
│   ├── ApiPlugin.cs                # Sends metrics via HTTP POST
│   └── FileLoggerPlugin.cs         # Saves metrics to a log file
├── Services/
│   ├── MonitoringService.cs        # Main monitoring loop
│   └── WindowsMetricsCollector.cs  # Reads CPU/RAM/Disk from Windows
├── appsettings.json                # Configuration
└── Program.cs                      # Entry point
```

---

## Features

- Reads CPU %, RAM usage, and Disk usage every N seconds
- Logs metrics to `system_log.txt` via FileLoggerPlugin
- Sends metrics to a configurable API endpoint via ApiPlugin (HTTP POST)
- Plugin-based architecture using interfaces

---

## Requirements

- Windows OS
- .NET 8.0 SDK
- Visual Studio 2022

---

## NuGet Packages Used

- `Microsoft.Extensions.Configuration.Json`
- `Microsoft.Extensions.DependencyInjection`
- `Microsoft.Extensions.Hosting`
- `System.Diagnostics.PerformanceCounter`

---

## Configuration

Edit `appsettings.json` to change the interval or API URL:

```json
{
  "IntervalSeconds": "5",
  "ApiUrl": "https://jsonplaceholder.typicode.com/posts"
}
```

---

## Prerequisites

Before running, make sure you have these installed on your PC:

- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community edition is free)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Windows OS (required for CPU/RAM/Disk reading)

## How to Run

1. Download or clone this repository
2. Open `SystemMonitor.sln` in Visual Studio 2022
3. Wait for NuGet packages to restore automatically
4. Press `F5` to build and run

**Expected output:**
```
Monitoring started. Interval: 5s. Press Ctrl+C to stop.

[14:32:01] CPU:12.4% | RAM:512/8192MB | Disk:120000/500000MB
[FileLogger] Saved to system_log.txt
[ApiPlugin] POST → 201
```

Press `Ctrl+C` to stop.

---

## Design Decisions & Challenges

I used interfaces (`IMonitorPlugin`, `ISystemMetricsCollector`) to keep each part of the code separate. This means the file logger and API plugin work independently, and adding a new plugin in the future is easy without changing existing code.

The main challenges were: `PerformanceCounter` in .NET 8 needs to be installed as a separate NuGet package, and `appsettings.json` must have **Copy to Output Directory** set to `Copy Always` — otherwise the app cannot find the config file when running.
