namespace OneLine.Models
{
    public class Mutable<T> : IMutable<T>
    {
        public Mutable()
        {
        }
        public Mutable(T item1)
        {
            Item1 = item1;
        }
        public T Item1 { get; set; }
    }
    public class Mutable<T1, T2> : IMutable<T1, T2>
    {
        public Mutable()
        {
        }
        public Mutable(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
    }
    public class Mutable<T1, T2, T3> : IMutable<T1, T2, T3>
    {
        public Mutable()
        {
        }
        public Mutable(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
    }
    public class Mutable<T1, T2, T3, T4> : IMutable<T1, T2, T3, T4>
    {
        public Mutable()
        {
        }
        public Mutable(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
    }
}
