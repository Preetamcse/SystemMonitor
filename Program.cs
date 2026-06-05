using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemMonitor.Interfaces;
using SystemMonitor.Plugins;
using SystemMonitor.Services;


var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json").Build();

var provider = new ServiceCollection()
    .AddSingleton<ISystemMetricsCollector, WindowsMetricsCollector>()
    .AddSingleton<IMonitorPlugin, FileLoggerPlugin>()
    .AddSingleton<IMonitorPlugin>(_ => new ApiPlugin(config["ApiUrl"] ?? "https://httpbin.org/post"))
    .BuildServiceProvider();

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

await new MonitoringService(
    provider.GetRequiredService<ISystemMetricsCollector>(),
    provider.GetServices<IMonitorPlugin>(),
    int.Parse(config["IntervalSeconds"] ?? "5")
).RunAsync(cts.Token);
