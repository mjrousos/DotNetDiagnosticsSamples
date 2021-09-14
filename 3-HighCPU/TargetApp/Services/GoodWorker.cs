using System.Threading.Tasks;

namespace TargetApp.Services
{
    /// <summary>
    /// "Fixed" worker that asynchronously awaits for the necessary time.
    /// Note that in real-world scenarios, some amount of CPU consumption is
    /// unavoidable and eventually CPU bottlenecks will constrain well-optimized apps.
    /// The goal of performance profiling and tuning should be to identify those
    /// host spots and optimize them as much as possible.
    /// </summary>
    public class GoodWorker
    {
        public static long StaticVariable { get; set; } = 0;

        // This updated worker keeps the mutual recursion pattern so
        // that call stacks will look similar to the slower version.
        public async Task DoWorkAsync(int ms)
        {
            if (ms <= 0)
            {
                return;
            }

            ms -= 50;

            await SpinFor50msAsync();

            await WorkHelperAsync(ms);
        }

        // WorkHelper is a clone of DoWork that is included
        // to make for more interesting call stacks.
        private async Task WorkHelperAsync(int ms)
        {
            if (ms <= 0)
            {
                return;
            }

            ms -= 50;

            await SpinFor50msAsync();

            await DoWorkAsync(ms);
        }

        private Task SpinFor50msAsync() => Task.Delay(50);
    }
}
