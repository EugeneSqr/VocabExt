namespace VX.Domain.Entities
{
    public interface ITag
    {
        int Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }
    }
}
