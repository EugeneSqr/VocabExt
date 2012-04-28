using VX.Service;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests.Mocks
{
    internal class RandomFacadeMock : IRandomFacade
    {
        public int PickRandomValue(int minValue, int maxValue)
        {
            return minValue;
        }
    }
}
