using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneDirectory.Api.Services;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;

namespace PhoneDirectory.Api.BackgroundServices;

public class BulkOperationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public BulkOperationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var contactService = scope.ServiceProvider.GetRequiredService<IContactService>();
        var queue = contactService.GetTaskQueue();

        while (!stoppingToken.IsCancellationRequested)
        {
            if (queue.TryDequeue(out var task))
            {
                try
                {
                    switch (task.Operation)
                    {
                        case "Create":
                            await contactService.CreateAsync((Contact)task.Data);
                            break;
                        case "Delete":
                            await contactService.DeleteAsync((int)task.Data);
                            break;
                        case "Disable":
                            await contactService.DisableAsync((int)task.Data);
                            break;
                    }
                }
                catch (Exception)
                {
                    // Log error (simplified for brevity)
                }
            }
            await Task.Delay(100, stoppingToken);
        }
    }
}
