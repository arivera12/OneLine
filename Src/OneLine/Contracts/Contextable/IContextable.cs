namespace OneLine.Contracts
{
    public interface IContextable<TContext>
    {
        public TContext Context { get; set; }
    }
}
