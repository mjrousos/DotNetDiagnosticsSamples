using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace TargetApp.Controllers
{
    public class WorkerController : ApiController
    {
        private static Account Account1 { get; set; } = new Account(100);
        private static Account Account2 { get; set; } = new Account(100);

        public WorkerController()
        {
        }

        [HttpPost]
        public async Task<IHttpActionResult> RunTaskAsync(decimal amount)
        {
            await Task.WhenAll(
                TransferAsync(Account1, Account2, amount),
                TransferAsync(Account2, Account1, amount));

            return StatusCode(HttpStatusCode.Accepted);
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


        [HttpPost]
        public async Task<IHttpActionResult> RunTask(decimal amount)
        {
            await Task.WhenAll(
                Task.Run(() => Transfer(Account1, Account2, amount)),
                Task.Run(() => Transfer(Account2, Account1, amount)));

            return StatusCode(HttpStatusCode.Accepted);
        }

        private void Transfer(Account sourceAccount, Account destinationAccount, decimal amount)
        {
            lock (sourceAccount)
            {
                sourceAccount.Balance -= amount;

                lock (destinationAccount)
                {
                    destinationAccount.Balance += amount;
                }
            }
        }
    }
}
