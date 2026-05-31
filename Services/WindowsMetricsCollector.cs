using System.Diagnostics;
using System.Runtime.InteropServices;
using SystemMonitor.Interfaces;
using SystemMonitor.Models;

namespace SystemMonitor.Services;

// Collects CPU, RAM, Disk metrics on Windows
public class WindowsMetricsCollector : ISystemMetricsCollector
{
   
    private readonly PerformanceCounter? _cpu = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? new PerformanceCounter("Processor", "% Processor Time", "_Total") : null;

    public WindowsMetricsCollector() => _cpu?.NextValue(); 

    public SystemMetrics Collect()
    {
        var drive = new DriveInfo(Path.GetPathRoot(Environment.CurrentDirectory) ?? "C:\\");
        var mem = GC.GetGCMemoryInfo();
        return new SystemMetrics
        {
            Cpu = Math.Round(_cpu?.NextValue() ?? 0, 1),
            RamUsed = Math.Round(Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0, 1),
            RamTotal = Math.Round(mem.TotalAvailableMemoryBytes / 1024.0 / 1024.0, 1),
            DiskUsed = Math.Round((drive.TotalSize - drive.TotalFreeSpace) / 1024.0 / 1024.0, 1),
            DiskTotal = Math.Round(drive.TotalSize / 1024.0 / 1024.0, 1)
        };
    }
}