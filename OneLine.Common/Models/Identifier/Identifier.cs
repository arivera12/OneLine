namespace OneLine.Models
{
    public class Identifier<T> : IIdentifier<T>
    {
        public virtual T Model { get; set; }
        public Identifier()
        { }
        public Identifier(T model)
        {
            Model = model;
        }
    }
}
