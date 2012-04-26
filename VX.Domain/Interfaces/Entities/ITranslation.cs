namespace VX.Domain.Interfaces.Entities
{
    public interface ITranslation
    {
        IWord Source { get; set; }

        IWord Target { get; set; }
    }
}
