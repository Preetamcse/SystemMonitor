using SystemMonitor.Models;

namespace SystemMonitor.Interfaces;

public interface IMonitorPlugin
{
    Task Execute(SystemMetrics metrics);
}