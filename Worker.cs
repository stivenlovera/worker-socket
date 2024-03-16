using worker_socket.utils;

namespace worker_socket;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    public Worker(
        ILogger<Worker> logger


        )
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        /* var request = new Request();
        await request.RequestFormData(); */
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


            await Task.Delay(5000, stoppingToken);

        }
    }
}
