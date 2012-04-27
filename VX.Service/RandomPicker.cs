using System;
using System.Collections.Generic;
using VX.Service.Interfaces;

namespace VX.Service
{
    public class RandomPicker : IRandomPicker
    {
        private readonly IRandomFacade randomFacade;
        
        public RandomPicker(IRandomFacade randomFacade)
        {
            this.randomFacade = randomFacade;
        }

        public IList<T> PickItems<T>(IList<T> list, int numberOfItems)
        {
            CheckIfInputListEmpty(list);

            if (numberOfItems < 0)
            {
                throw new ArgumentOutOfRangeException("numberOfItems",
                    "Number of items to select must be greater than 0");
            }

            if (numberOfItems == 1)
            {
                return new List<T> {PickItem(list)};
            }

            var resultList = new List<T>();
            int itemsSelected = 0;
            int itemsCount = list.Count;
            while(itemsSelected < Math.Min(numberOfItems, itemsCount))
            {
                var item = PickRandomItem(list);
                resultList.Add(item);
                list.Remove(item);
                itemsSelected++;
            }

            return resultList;
        }

        public IList<T> PickItems<T>(IList<T> list, int numberOfItems, IList<T> blackList)
        {
            CheckIfInputListEmpty(list);
            if (blackList == null || blackList.Count == 0)
            {
                return PickItems(list, numberOfItems);
            }
            
            if (list.Equals(blackList))
            {
                return new List<T>();
            }

            foreach (var blackListItem in blackList)
            {
                list.Remove(blackListItem);
            }

            return list.Count == 0 
                ? new List<T>() 
                : PickItems(list, numberOfItems);

        }

        public T PickItem<T>(IList<T> list)
        {
            CheckIfInputListEmpty(list);
            return PickRandomItem(list);
        }

        private T PickRandomItem<T>(IList<T> list)
        {
            int position = randomFacade.PickRandomValue(0, list.Count);
            return list[position];
        }

        private static void CheckIfInputListEmpty<T>(ICollection<T> list)
        {
            if (list == null || list.Count == 0)
            {
                // TODO: localize
                throw new ArgumentNullException(
                    "list",
                    string.Format("Can't pick an item from empty list. Type: {0}", typeof (T)));
            }
        }
    }
}