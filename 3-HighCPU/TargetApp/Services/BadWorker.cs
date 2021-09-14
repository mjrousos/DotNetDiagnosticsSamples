using System;

namespace TargetApp.Services
{
    /// <summary>
    /// Simple 'time waster' class that consumes CPU with repeated DateTime.Now calls
    /// and arithmetic. Based on the PerfView tutorial sample.
    /// https://github.com/microsoft/perfview/blob/main/src/PerfView/SupportFiles/Tutorial.cs
    /// </summary>
    public class BadWorker
    {
        public static long StaticVariable { get; set; } = 0;

        // Simple compute-bound method useful for testing CPU profilers.
        public void DoWork(int ms)
        {
            if (ms <= 0)
            {
                return;
            }

            ms -= 50;

            SpinFor50ms();

            WorkHelper(ms);
        }

        // WorkHelper is a clone of DoWork that is included
        // to make for more interesting call stacks.
        private void WorkHelper(int ms)
        {
            if (ms <= 0)
            {
                return;
            }

            ms -= 50;

            SpinFor50ms();

            DoWork(ms);
        }

        // SpinFor50ms repeatedly calls DateTime.Now until for
        // 50ms.  It also does some work of its own in this
        // methods so we get some exclusive time to look at.  
        private void SpinFor50ms()
        {
            DateTime start = DateTime.Now;
            for (; ; )
            {
                if ((DateTime.Now - start).TotalMilliseconds > 50)
                    break;

                // Do some work in this routine as well.   
                for (int i = 0; i < 10; i++)
                    StaticVariable += i;
            }

        }
    }
}
