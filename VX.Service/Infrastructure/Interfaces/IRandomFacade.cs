namespace VX.Service.Infrastructure.Interfaces
{
    public interface IRandomFacade
    {
        int PickRandomValue(int minValue, int maxValue);
    }
}