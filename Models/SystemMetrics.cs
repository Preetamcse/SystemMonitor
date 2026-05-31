namespace SystemMonitor.Models;


public class SystemMetrics
{
    public double Cpu, RamUsed, RamTotal, DiskUsed, DiskTotal;
    public DateTime Time = DateTime.Now;
}