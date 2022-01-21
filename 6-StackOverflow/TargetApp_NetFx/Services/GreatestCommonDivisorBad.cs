using System;

namespace TargetApp
{
    public class GreatestCommonDivisorBad
    {
        // Euclid's original recursive algorithm
        public long GCD(long x, long y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);

            if (x == 0)
            {
                return y;
            }

            if (y == 0)
            {
                return x;
            }

            return x > y
                ? GCD(y, x - y)
                : GCD(x, y - x);
        }
    }
}