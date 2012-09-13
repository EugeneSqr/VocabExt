namespace VX.Service.Infrastructure.Factories.SearchStrings
{
    public interface ISearchStringFactory
    {
        int StartSearchLettersCount { get; }

        string Create(string inputString);
    }
}