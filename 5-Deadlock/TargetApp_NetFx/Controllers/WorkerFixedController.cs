using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace TargetApp.Controllers
{
    public class WorkerFixedController : ApiController
    {
        private static Account Account1 { get; set; } = new Account(100);
        private static Account Account2 { get; set; } = new Account(100);

        public WorkerFixedController()
        {
        }

        [HttpPost]
        public async Task<IHttpActionResult> RunTaskAsync(decimal amount)
        {
            await Task.WhenAll(
                TransferFixedAsync(Account1, Account2, amount),
                TransferFixedAsync(Account2, Account1, amount));

            return StatusCode(HttpStatusCode.Accepted);
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

        [HttpPost]
        public async Task<IHttpActionResult> RunTask(decimal amount)
        {
            await Task.WhenAll(
                Task.Run(() => TransferFixed(Account1, Account2, amount)),
                Task.Run(() => TransferFixed(Account2, Account1, amount)));

            return StatusCode(HttpStatusCode.Accepted);
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
}
