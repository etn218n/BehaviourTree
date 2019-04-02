public class Inverter : INode
{
    private INode child;

    public Inverter(INode child) { this.child = child; }

    public NodeStatus Tick()
    {
        NodeStatus status = child.Tick();

        if (status == NodeStatus.Sucess)
            return NodeStatus.Failure;
        else if (status == NodeStatus.Failure)
            return NodeStatus.Sucess;
        else
            return NodeStatus.Running;
    }
}
