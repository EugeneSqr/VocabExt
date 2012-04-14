using System;

using VX.Service.Repositories;

namespace VX.Service
{
    public class VocabExtService : IVocabExtService
    {
        public string GetData(int value)
        {
            // return string.Format("You entered: {0}", value);            
            return new LanguageRepository().GetById(1).Name;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
