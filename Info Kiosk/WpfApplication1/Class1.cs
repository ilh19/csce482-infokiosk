using System;
using System.Collections.Generic;

namespace WpfApplication1
{
    public class CircularArray<T>
    {
        int MaxSize;
        List<T> list;

        public CircularArray(int MaxSize)
        {
            this.MaxSize = MaxSize;
            list = new List<T>();
        }

        public void add(T o)
        {
            if (list.Count == MaxSize)
                list.RemoveAt(0);
            list.Add(o);
        }

        public bool contains(T o)
        {
            return list.Contains(o);
        }
    }
}