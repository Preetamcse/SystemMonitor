using SystemMonitor.Interfaces;

namespace SystemMonitor.Services;

public class MonitoringService(ISystemMetricsCollector collector, IEnumerable<IMonitorPlugin> plugins, int intervalSeconds)

{
    public async Task RunAsync(CancellationToken ct)
    {
        Console.WriteLine($"Monitoring started. Interval: {intervalSeconds}s. Press Ctrl+C to stop.\n");

        while (!ct.IsCancellationRequested)
        {
            var metrics = collector.Collect();
             Console.WriteLine($"Time: [{metrics.Time:HH:mm:ss}]");
             Console.WriteLine($"CPU: {metrics.Cpu}%");
             Console.WriteLine($"RAM: {metrics.RamUsed}/{metrics.RamTotal} MB");
             Console.WriteLine($"Disk: {metrics.DiskUsed}/{metrics.DiskTotal} MB");
            
            foreach (var plugin in plugins) await plugin.Execute(metrics);

            Console.WriteLine();
            await Task.Delay(intervalSeconds * 1000, ct);
        }
    }
}
