using System;

namespace DP_Strategy
{
    class MainApp
    {
        static void Main()
        {
            Vector v = new Vector(new QuickSort());
            v.Sort();

            v.Strategy = new BubbleSort();
            v.Sort();

            v.Strategy = new InsertionSort();
            v.Sort();

            Console.Read();
        }
    }

    class Vector
    {
        // Ссылка на объект, занимающийся сортировкой
        SortStrategy strategy;

        public SortStrategy Strategy
        {
            get
            {
                return strategy;
            }
            set
            {
                strategy = value;
            }
        }

        int[] numbers = new int[100];

        // Constructor 
        public Vector(SortStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void Sort()
        {
            // Вызов метода Sort, объекта сортировки, ссылка на который хранится в классе
            strategy.Sort();
        }
    }
}
