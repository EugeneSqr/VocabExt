using System;

namespace VX.Service
{
    public class RandomFacade : IRandomFacade
    {
        private readonly Random generator;

        public RandomFacade()
        {
            generator = new Random();
        }

        public int PickRandomValue(int minValue, int maxValue)
        {
            return generator.Next(minValue, maxValue);
        }
    }
}