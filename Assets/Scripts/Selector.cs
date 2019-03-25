using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : INode
{
    private readonly INode[] nodes;
    private int currentIndex;

    public Selector(params INode[] nodes)
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
            case NodeStatus.Sucess:  currentIndex = 0; return NodeStatus.Sucess;
            case NodeStatus.Failure: currentIndex++; break;
        }

        if (currentIndex >= nodes.Length)
        {
            currentIndex = 0;
            return NodeStatus.Failure;
        }

        return NodeStatus.Running;
    }
}
