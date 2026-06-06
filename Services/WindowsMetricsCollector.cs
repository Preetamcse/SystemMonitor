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
       
         double cpu = _cpu.NextValue();
         double ramUsed =  Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0;     

         double ramTotal =  mem.TotalAvailableMemoryBytes / 1024.0 / 1024.0;
     
         double diskUsed =   (drive.TotalSize - drive.TotalFreeSpace) / 1024.0 / 1024.0;

         double diskTotal =   drive.TotalSize / 1024.0 / 1024.0;

          return new SystemMetrics
          {
              Cpu = Math.Round(cpu, 1),
              RamUsed = Math.Round(ramUsed, 1),
              RamTotal = Math.Round(ramTotal, 1),
              DiskUsed = Math.Round(diskUsed, 1),
              DiskTotal = Math.Round(diskTotal, 1)
          };
    }
}
