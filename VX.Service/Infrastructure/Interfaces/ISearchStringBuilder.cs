namespace VX.Service.Infrastructure.Interfaces
{
    public interface ISearchStringBuilder
    {
        int StartSearchLettersCount { get; }

        string BuildSearchString(string inputString);
    }
}