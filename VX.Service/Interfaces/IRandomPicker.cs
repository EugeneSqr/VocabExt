using System.Collections.Generic;

namespace VX.Service.Interfaces
{
    public interface IRandomPicker
    {
        IList<T> PickItems<T>(IList<T> list, int numberOfItems);

        T PickItem<T>(IList<T> list);
    }
}