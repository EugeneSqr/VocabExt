using System.Text;
using VX.Service.Factories.Interfaces;

namespace VX.Service.Factories
{
    public class CacheKeyFactory : ICacheKeyFactory
    {
        private const string NameParametersTemplate = "{0}:{1}";
        
        public string BuildKey(string serviceName, int[] parameters)
        {
            StringBuilder intKeyPart = new StringBuilder();
            foreach (int parameter in parameters)
            {
                intKeyPart.Append(parameter).Append("-");
            }

            return string.Format(NameParametersTemplate, serviceName, intKeyPart);
        }

        public string BuildKey(string serviceName, string parameter)
        {
            return string.Format(NameParametersTemplate, serviceName, parameter);
        }
    }
}