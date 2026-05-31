using SystemMonitor.Interfaces;
using SystemMonitor.Models;

namespace SystemMonitor.Plugins;


public class FileLoggerPlugin : IMonitorPlugin
{
    public async Task Execute(SystemMetrics metrics)
    {
        await File.AppendAllTextAsync("system_log.txt",
            $"[{metrics.Time:HH:mm:ss}] CPU:{metrics.Cpu}% RAM:{metrics.RamUsed}/{metrics.RamTotal}MB Disk:{metrics.DiskUsed}/{metrics.DiskTotal}MB\n");
        Console.WriteLine("[FileLogger] Saved to system_log.txt");
    }
}