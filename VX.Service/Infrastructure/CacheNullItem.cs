namespace VX.Service.Infrastructure
{
    public sealed class CacheNullItem
    {
        private static readonly CacheNullItem Internal = new CacheNullItem();

        private CacheNullItem()
        {
        }

        public static CacheNullItem Value
        {
            get { return Internal; }
        }
    }
}