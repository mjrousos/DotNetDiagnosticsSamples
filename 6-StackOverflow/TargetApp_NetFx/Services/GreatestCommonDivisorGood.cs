using System;

namespace TargetApp
{
    public class GreatestCommonDivisorGood
    {
        // Improved iterative Euclidean algorithm
        public long GCD(long x, long y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);

            while (y != 0)
            {
                (x, y) = (y, x % y);
            }

            return x;
        }
    }
}