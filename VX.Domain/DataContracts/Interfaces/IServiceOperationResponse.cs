namespace VX.Domain.DataContracts.Interfaces
{
    public interface IServiceOperationResponse
    {
        string ErrorMessage { get; }

        bool Status { get; }
    }
}
