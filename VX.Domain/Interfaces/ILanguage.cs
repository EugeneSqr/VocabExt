namespace VX.Domain.Interfaces
{
    public interface ILanguage
    {
        int Id { get; set; }
        
        string Name { get; set; }

        string Abbreviation { get; set; }
    }
}
