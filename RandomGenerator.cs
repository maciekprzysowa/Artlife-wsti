using System;

namespace TestApp1
{
    public static class RandomGenerator
    {
        internal static Random rnd = new Random();

        public static int GetRandom(int min, int max)
        {
            return rnd.Next(min, max);
        }
    }
}