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
            Console.WriteLine($"[{metrics.Time:HH:mm:ss}] CPU:{metrics.Cpu}% | RAM:{metrics.RamUsed}/{metrics.RamTotal}MB | Disk:{metrics.DiskUsed}/{metrics.DiskTotal}MB");

            foreach (var plugin in plugins) await plugin.Execute(metrics);

            Console.WriteLine();
            await Task.Delay(intervalSeconds * 1000, ct);
        }
    }
}