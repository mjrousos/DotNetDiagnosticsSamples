using Microsoft.AspNetCore.Mvc;

namespace TargetApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkerController : ControllerBase
{
    private static Account Account1 { get; set; } = new Account(100);
    private static Account Account2 { get; set; } = new Account(100);

    private readonly ILogger<WorkerController> _logger;

    public WorkerController(ILogger<WorkerController> logger)
    {
        _logger = logger;
    }

    [HttpPost("RunTaskFixed/{amount}")]
    public async Task<ActionResult> RunTaskFixed(decimal amount)
    {
        await Task.WhenAll(
            TransferFixed(Account1, Account2, amount),
            TransferFixed(Account2, Account1, amount));

        return Accepted();
    }

    [HttpPost("RunTask/{amount}")]
    public async Task<ActionResult> RunTask(decimal amount)
    {
        await Task.WhenAll(
            Transfer(Account1, Account2, amount),
            Transfer(Account2, Account1, amount));

        return Accepted();
    }

    private async Task Transfer(Account sourceAccount, Account destinationAccount, decimal amount)
    {
        await sourceAccount.SynchronizationSemaphore.WaitAsync();

        try
        {
            sourceAccount.Balance -= amount;
            await destinationAccount.SynchronizationSemaphore.WaitAsync();

            try
            {
                destinationAccount.Balance += amount;
            }
            finally
            {
                destinationAccount.SynchronizationSemaphore.Release();
            }
        }
        finally
        {
            sourceAccount.SynchronizationSemaphore.Release();
        }
    }

    private async Task TransferFixed(Account sourceAccount, Account destinationAccount, decimal amount)
    {
        await sourceAccount.SynchronizationSemaphore.WaitAsync();

        try
        {
            sourceAccount.Balance -= amount;
        }
        finally
        {
            sourceAccount.SynchronizationSemaphore.Release();
        }

        await destinationAccount.SynchronizationSemaphore.WaitAsync();

        try
        {
            destinationAccount.Balance += amount;
        }
        finally
        {
            destinationAccount.SynchronizationSemaphore.Release();
        }

    }
}
