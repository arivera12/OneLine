namespace OneLine.Models
{
    /// <summary>
    /// Defines a generic mutable structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMutable<T>
    {
        T Item1 { get; set; }
    }
    /// <summary>
    /// Defines a generic mutable structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMutable<T1, T2>
    {
        T1 Item1 { get; set; }
        T2 Item2 { get; set; }
    }
    /// <summary>
    /// Defines a generic mutable structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMutable<T1, T2, T3>
    {
        T1 Item1 { get; set; }
        T2 Item2 { get; set; }
        T3 Item3 { get; set; }
    }
    /// <summary>
    /// Defines a generic mutable structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMutable<T1, T2, T3, T4>
    {
        T1 Item1 { get; set; }
        T2 Item2 { get; set; }
        T3 Item3 { get; set; }
        T4 Item4 { get; set; }
    }
}
