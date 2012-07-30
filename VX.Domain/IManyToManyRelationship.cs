namespace VX.Domain
{
    public interface IManyToManyRelationship
    {
        int Id { get; set; }

        int SourceId { get; set; }

        int TargetId { get; set; }
    }
}
