using System.Collections;
using System.Collections.Generic;

public class Sequence : INode
{
    private readonly INode[] nodes;
    private int currentIndex;

    public Sequence(params INode[] nodes)
    {
        this.nodes = nodes;

        if (nodes.Length != 0) currentIndex = 0;
    }

    public NodeStatus Tick()
    {
        NodeStatus status = nodes[currentIndex].Tick();

        switch (status)
        {
            case NodeStatus.Running: return NodeStatus.Running;
            case NodeStatus.Failure: currentIndex = 0; return NodeStatus.Failure;
            case NodeStatus.Sucess:  currentIndex++; break;
        }

        if (currentIndex >= nodes.Length)
        {
            currentIndex = 0;
            return NodeStatus.Sucess;
        }

        return NodeStatus.Running;
    }
}
