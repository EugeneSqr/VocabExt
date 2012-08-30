namespace VX.Domain.Entities
{
    public interface ITranslation
    {
        int Id { get; set; }

        IWord Source { get; set; }

        IWord Target { get; set; }
    }
}
