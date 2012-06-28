using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests.Mocks
{
    public class RandomFacadeMock : IRandomFacade
    {
        public int PickRandomValue(int minValue, int maxValue)
        {
            return minValue;
        }
    }
}
