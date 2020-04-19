namespace OneLine.Models
{
    public interface IIdentifier<T>
    {
        T Model { get; set; }
    }
}
