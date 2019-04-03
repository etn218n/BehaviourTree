public class Succeeder : INode
{
    private INode child;

    public Succeeder(INode child) { this.child = child; }

    public NodeStatus Tick()
    {
        NodeStatus status = child.Tick();

        if (status == NodeStatus.Running) return NodeStatus.Running;

        return NodeStatus.Sucess;
    }
}
