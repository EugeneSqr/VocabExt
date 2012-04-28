using System.Collections.Generic;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface IRandomPicker
    {
        IList<T> PickItems<T>(IList<T> list, int numberOfItems);

        IList<T> PickItems<T>(IList<T> list, int numberOfItems, IList<T> blackList);

        T PickItem<T>(IList<T> list);
    }
}