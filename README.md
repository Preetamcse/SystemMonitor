# SystemMonitor - C# Console Application

A Simple C# Console App That Keeps An Eye On Your Computer's CPU, RAM, And Disk Usage In Real Time. Every Few Seconds It Logs The Readings To A File And Sends Them To An API Endpoint.

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

[14:32:01] 
CPU:12.4% |
RAM:512/8192MB | 
Disk:120/500GB
[FileLogger] Saved to system_log.txt
[ApiPlugin] POST → 201
```

Press `Ctrl+C` to stop.

---

## Design Decisions & Challenges

I used interfaces (`IMonitorPlugin`, `ISystemMetricsCollector`) to keep each part of the code separate. This means the file logger and API plugin work independently, and adding a new plugin in the future is easy without changing existing code.

The main challenge was retrieving system health data from the computer. This involved finding the right .NET APIs and system interfaces to collect metrics such as CPU usage, memory usage, and disk information accurately and efficiently.
