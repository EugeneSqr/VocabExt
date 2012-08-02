using System.Web.Script.Serialization;

namespace VX.Service.Factories.Interfaces
{
    public interface IJavaScriptConvertersFactory
    {
        JavaScriptConverter Build(string converterName);
    }
}