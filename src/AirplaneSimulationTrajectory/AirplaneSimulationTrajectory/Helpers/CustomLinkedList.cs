using System;
using System.Collections.Generic;

namespace AirplaneSimulationTrajectory.Helpers
{
    [Obsolete("Maybe I will make a decision to delete this class")]
    public class CustomLinkedList<T> : List<T>
    {
        private int _currentIndex;

        public CustomLinkedList(IEnumerable<T> collection) : base(collection)
        {
        }

        public CustomLinkedList(params T[] paramList) : base(paramList)
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