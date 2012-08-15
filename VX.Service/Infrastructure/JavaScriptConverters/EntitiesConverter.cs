using System.Web.Script.Serialization;
using VX.Service.Factories.Interfaces;

namespace VX.Service.Infrastructure.JavaScriptConverters
{
    public abstract class EntitiesConverter : JavaScriptConverter
    {
        protected readonly IEntitiesFactory EntitiesFactory;

        protected EntitiesConverter(IEntitiesFactory entitiesFactory)
        {
            EntitiesFactory = entitiesFactory;
        }
    }
}