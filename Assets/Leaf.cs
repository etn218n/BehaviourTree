public abstract class Leaf<T> : INode
{
    protected readonly T context;

    public Leaf(T context) { this.context = context; }

    public virtual NodeStatus Tick() { return NodeStatus.Failure; }
}
