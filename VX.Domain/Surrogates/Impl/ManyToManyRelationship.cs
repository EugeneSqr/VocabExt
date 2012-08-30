namespace VX.Domain.Surrogates.Impl
{
    public class ManyToManyRelationship : IManyToManyRelationship
    {
        public int Id { get; set; }

        public int SourceId { get; set; }

        public int TargetId { get; set; }
    }
}
