using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class ProductBackgroundService : BackgroundService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductBackgroundService> _logger;
    private const string ApiUrl = "http://localhost:5000/api/Product";
    private const string LogFilePath = "C:\\Logs\\ProductData.log";

    public ProductBackgroundService(HttpClient httpClient, ILogger<ProductBackgroundService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiUrl, stoppingToken);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                
                LogData(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product data");
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }

    private void LogData(string data)
    {
        try
        {
            File.AppendAllText(LogFilePath, $"{DateTime.Now}: {data}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error writing to log file");
        }
    }
}
