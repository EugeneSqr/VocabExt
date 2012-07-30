namespace VX.Domain
{
    public class ManyToManyRelationship : IManyToManyRelationship
    {
        public int Id { get; set; }

        public int SourceId { get; set; }

        public int TargetId { get; set; }
    }
}
