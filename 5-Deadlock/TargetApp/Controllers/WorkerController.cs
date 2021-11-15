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

    [HttpPost("RunTaskFixedAsync/{amount}")]
    public async Task<ActionResult> RunTaskFixedAsync(decimal amount)
    {
        await Task.WhenAll(
            TransferFixedAsync(Account1, Account2, amount),
            TransferFixedAsync(Account2, Account1, amount));

        return Accepted();
    }

    [HttpPost("RunTaskAsync/{amount}")]
    public async Task<ActionResult> RunTaskAsync(decimal amount)
    {
        await Task.WhenAll(
            TransferAsync(Account1, Account2, amount),
            TransferAsync(Account2, Account1, amount));

        return Accepted();
    }

    private async Task TransferAsync(Account sourceAccount, Account destinationAccount, decimal amount)
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

    private async Task TransferFixedAsync(Account sourceAccount, Account destinationAccount, decimal amount)
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

    [HttpPost("RunTaskFixed/{amount}")]
    public async Task<ActionResult> RunTaskFixed(decimal amount)
    {
        await Task.WhenAll(
            Task.Run(() => TransferFixed(Account1, Account2, amount)),
            Task.Run(() => TransferFixed(Account2, Account1, amount)));

        return Accepted();
    }

    [HttpPost("RunTask/{amount}")]
    public async Task<ActionResult> RunTask(decimal amount)
    {
        await Task.WhenAll(
            Task.Run(() => Transfer(Account1, Account2, amount)),
            Task.Run(() => Transfer(Account2, Account1, amount)));

        return Accepted();
    }

    private void Transfer(Account sourceAccount, Account destinationAccount, decimal amount)
    {
        lock(sourceAccount)
        {
            sourceAccount.Balance -= amount;

            lock(destinationAccount)
            {
                destinationAccount.Balance += amount;
            }
        }
    }

    private void TransferFixed(Account sourceAccount, Account destinationAccount, decimal amount)
    {
        lock (sourceAccount)
        {
            sourceAccount.Balance -= amount;
        }

        lock (destinationAccount)
        {
            destinationAccount.Balance += amount;
        }
    }
}
