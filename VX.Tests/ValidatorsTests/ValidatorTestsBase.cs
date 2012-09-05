using Autofac;
using NUnit.Framework;
using VX.Domain.Responses;
using VX.Domain.Surrogates;
using VX.Service.Infrastructure.Factories.Responses;
using VX.Tests.Mocks;

namespace VX.Tests.ValidatorsTests
{
    public abstract class ValidatorTestsBase<TType, TImplementation> : DataLayerTestsBase<TType, TImplementation>
    {
        protected ValidatorTestsBase()
        {
            ContainerBuilder.RegisterType<ResponsesFactoryMock>()
                .As<IResponsesFactory>()
                .InstancePerLifetimeScope();
        }

        protected void CheckValidationResult(bool status, string errorMessage, IOperationResponse actual)
        {
            Assert.AreEqual(status, actual.Status);
            Assert.AreEqual((int)ServiceOperationAction.Validate, actual.OperationActionCode);
            Assert.AreEqual(errorMessage, actual.ErrorMessage);
        }
    }
}
