namespace TargetApp
{
    public class Account
    {
        public decimal Balance { get; set; }

        // This is a bad architecture. Do not do this.
        public SemaphoreSlim SynchronizationSemaphore { get; set; }

        public Account(decimal initialBalance)
        {
            Balance = initialBalance;
            SynchronizationSemaphore = new SemaphoreSlim(1, 1);
        }
    }
}