using System.Text;
using System.Text.Json;
using SystemMonitor.Interfaces;
using SystemMonitor.Models;

namespace SystemMonitor.Plugins;


public class ApiPlugin(string url) : IMonitorPlugin
{
    private readonly HttpClient _http = new();

    public async Task Execute(SystemMetrics metrics)
    {
        var json = JsonSerializer.Serialize(new { cpu = metrics.Cpu, ram_used = metrics.RamUsed, disk_used = metrics.DiskUsed });
        try
        {
            var response = await _http.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            Console.WriteLine($"[ApiPlugin] POST → {(int)response.StatusCode}");
        }
        catch (Exception ex) { Console.WriteLine($"[ApiPlugin] Error: {ex.Message}"); }
    }
}