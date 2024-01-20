using System.Collections.Generic;

namespace AirplaneSimulationTrajectory.Helpers
{
    public class CustomLinkedList<T> : List<T>
    {
        public T Current => this[_currentIndex];

        private int _currentIndex = 0;

        public T Next
        {
            get
            {
                if (this.Count == 0) return default(T);
                if (_currentIndex < this.Count - 1)
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

        public CustomLinkedList(IEnumerable<T> collection) : base(collection)
        {
        }

        public CustomLinkedList(params T[] paramList) : base(paramList)
        {
        }

        public void AddList(params T[] paramList)
        {
            base.Clear();
            base.AddRange(paramList);
        }
    }
}