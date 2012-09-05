using System.IO;
using System.Text;

namespace VX.Tests.FactoriesTests
{
    public abstract class StreamSerializationTests<TType, TImplementation> : TestsBase<TType, TImplementation>
    {
        protected static MemoryStream GetStream(string source)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(source));
        }
    }
}
