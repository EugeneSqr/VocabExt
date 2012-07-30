namespace VX.Domain
{
    public interface IServiceOperationResponse
    {
        string ErrorMessage { get; }

        bool Status { get; }
    }
}
