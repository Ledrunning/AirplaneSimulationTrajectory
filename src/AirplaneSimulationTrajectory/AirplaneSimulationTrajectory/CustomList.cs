using System.Collections.Generic;

namespace AirplaneSimulationTrajectory
{
    public class CustomList<T> : List<T>
    {
        private int _currentIndex;

        public CustomList(IEnumerable<T> collection) : base(collection)
        {
        }

        public CustomList(params T[] paramList) : base(paramList)
        {
        }

        public T Current => this[_currentIndex];

        public T Next
        {
            get
            {
                if (Count == 0)
                {
                    return default;
                }

                if (_currentIndex < Count - 1)
                {
                    _currentIndex++;
                }
                else
                {
                    _currentIndex = 0;
                }

                return this[_currentIndex];
            }
        }

        public void AddList(params T[] paramList)
        {
            Clear();
            AddRange(paramList);
        }
    }
}