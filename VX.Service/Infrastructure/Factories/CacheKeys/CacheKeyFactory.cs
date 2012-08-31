using System;
using System.Text;

namespace VX.Service.Infrastructure.Factories.CacheKeys
{
    [RegisterService]
    public class CacheKeyFactory : ICacheKeyFactory
    {
        private const string NameParametersTemplate = "{0}:{1}";
        
        public string BuildKey(string serviceName, int[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            StringBuilder intKeyPart = new StringBuilder();
            foreach (int parameter in parameters)
            {
                intKeyPart.Append(parameter).Append("-");
            }

            return string.Format(NameParametersTemplate, serviceName, intKeyPart);
        }

        public string BuildKey(string serviceName, int parameter)
        {
            return string.Format(NameParametersTemplate, serviceName, parameter);
        }

        public string BuildKey(string serviceName, string parameter)
        {
            return string.Format(NameParametersTemplate, serviceName, parameter);
        }

        public string BuildKey(string serviceName, int[] parameters, bool flag)
        {
            var builder = new StringBuilder(BuildKey(serviceName, parameters));
            return builder.Append("flag:").Append(flag).ToString();
        }
    }
}