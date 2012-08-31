using System;
using System.Collections.Generic;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    [RegisterService]
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
                return new List<T> { PickItem(list) };
            }

            var resultList = new List<T>();
            var workList = new List<T>();
            workList.AddRange(list);
            int itemsSelected = 0;
            int itemsCount = workList.Count;
            while(itemsSelected < Math.Min(numberOfItems, itemsCount))
            {
                var item = PickRandomItem(workList);
                resultList.Add(item);
                workList.Remove(item);
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

            var workList = new List<T>();
            workList.AddRange(list);
            foreach (var blackListItem in blackList)
            {
                workList.Remove(blackListItem);
            }

            return workList.Count == 0 
                ? new List<T>() 
                : PickItems(workList, numberOfItems);
        }

        public T PickItem<T>(IList<T> list)
        {
            CheckIfInputListEmpty(list);
            return PickRandomItem(list);
        }

        public int PickInsertIndex<T>(IList<T> list)
        {
            if (list == null)
            {
                // todo localize
                throw new ArgumentNullException("list", "Can't pick index to insert from null list");
            }

            return randomFacade.PickRandomValue(0, list.Count);
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