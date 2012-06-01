namespace VX.Service.Infrastructure.Interfaces
{
    public interface IServiceSettings
    {
        string ConnectionString { get; }

        int DefaultTasksCount { get; }
    }
}