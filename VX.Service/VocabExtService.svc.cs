using System;
using Autofac;
using VX.Domain.Interfaces;

namespace VX.Service
{
    public class VocabExtService : IVocabExtService
    {
        private readonly ILanguagesRepository languagesRepository;

        public VocabExtService()
        {
            languagesRepository = Initializer.Container.Resolve<ILanguagesRepository>();
        }

        public string GetData(int value)
        {
            // return string.Format("You entered: {0}", value);            
            return languagesRepository.GetById(1).Name;
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
