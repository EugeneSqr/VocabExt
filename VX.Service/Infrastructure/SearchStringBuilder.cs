using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    [RegisterService]
    public class SearchStringBuilder : ISearchStringBuilder
    {
        // TODO: to config
        private const int MinimalLettersCount = 2;

        public int StartSearchLettersCount
        {
            get { return MinimalLettersCount; }
        }

        public string BuildSearchString(string inputString)
        {
            return string.IsNullOrEmpty(inputString) || inputString.Trim().Length < MinimalLettersCount
                       ? string.Empty
                       : inputString.Substring(0, MinimalLettersCount);
        }
    }
}