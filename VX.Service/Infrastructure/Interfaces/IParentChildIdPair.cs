namespace VX.Service.Infrastructure.Interfaces
{
    public interface IParentChildIdPair
    {
        int ParentId { get; }

        int ChildId { get; }
    }
}