namespace VX.Service.Infrastructure.Factories.SearchStrings
{
    [RegisterService]
    public class SearchStringFactory : ISearchStringFactory
    {
        // TODO: to config
        private const int MinimalLettersCount = 2;

        public int StartSearchLettersCount
        {
            get { return MinimalLettersCount; }
        }

        public string Create(string inputString)
        {
            return string.IsNullOrEmpty(inputString) || inputString.Trim().Length < MinimalLettersCount
                       ? string.Empty
                       : inputString.Substring(0, MinimalLettersCount);
        }
    }
}