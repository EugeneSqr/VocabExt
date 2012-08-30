using NUnit.Framework;
using VX.Domain;
using VX.Domain.Surrogates;

namespace VX.Tests.ValidatorsTests
{
    public abstract class ValidatorTestsBase<TType, TImplementation> : DataLayerTestsBase<TType, TImplementation>
    {
        protected void CheckValidationResult(bool status, string errorMessage, IServiceOperationResponse actual)
        {
            Assert.AreEqual(status, actual.Status);
            Assert.AreEqual((int)ServiceOperationAction.Validate, actual.OperationActionCode);
            Assert.AreEqual(errorMessage, actual.ErrorMessage);
        }
    }
}
