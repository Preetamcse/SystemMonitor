using SystemMonitor.Models;

namespace SystemMonitor.Interfaces;


public interface ISystemMetricsCollector
{
    SystemMetrics Collect();
}