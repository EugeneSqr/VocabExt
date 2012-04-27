using System;
using VX.Service.Interfaces;

namespace VX.Service
{
    internal class RandomFacade : IRandomFacade
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