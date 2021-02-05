using OneLine.Contracts;

namespace OneLine.Bases
{
    public class BaseContext<TContext> : IContextable<TContext>
    {
        public TContext Context { get; set; }
    }
}
