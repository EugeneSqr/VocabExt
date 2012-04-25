namespace VX.Domain.Interfaces
{
    public interface IEntitiesFactory
    {
        ILanguage BuildLanguage(Language language);
    }
}
