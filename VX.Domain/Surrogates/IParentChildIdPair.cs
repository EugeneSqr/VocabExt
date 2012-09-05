namespace VX.Domain.Surrogates
{
    public interface IParentChildIdPair
    {
        int ParentId { get; }

        int ChildId { get; }
    }
}