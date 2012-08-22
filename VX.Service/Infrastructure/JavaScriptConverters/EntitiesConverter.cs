using System.Web.Script.Serialization;
using VX.Service.Infrastructure.Factories.Adapters;

namespace VX.Service.Infrastructure.JavaScriptConverters
{
    public abstract class EntitiesConverter : JavaScriptConverter
    {
        protected readonly IAdapterFactory EntitiesFactory;

        protected EntitiesConverter(IAdapterFactory entitiesFactory)
        {
            EntitiesFactory = entitiesFactory;
        }
    }
}