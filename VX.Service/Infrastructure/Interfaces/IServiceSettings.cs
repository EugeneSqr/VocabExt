namespace VX.Service.Infrastructure.Interfaces
{
    public interface IServiceSettings
    {
        string ConnectionString { get; }

        string DomainDatabaseName { get; }

        int DefaultTasksCount { get; }

        int CacheSlidingExpirationSeconds { get; }
    }
}