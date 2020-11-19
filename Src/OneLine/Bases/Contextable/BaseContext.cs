using OneLine.Contracts;

namespace OneLine.Bases
{
    public class BaseContext<TContext> : IContextable<TContext>
    {
        public virtual TContext Context { get; set; }
    }
}
