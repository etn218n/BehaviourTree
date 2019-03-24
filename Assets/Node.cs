public enum NodeStatus
{
    Running,
    Sucess,
    Failure
}

public interface INode
{
    NodeStatus Tick();
}
