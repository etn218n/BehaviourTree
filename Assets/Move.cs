using UnityEngine;
using System.Collections.Generic;

public class FindDestination : Leaf<Testing>
{
    private Transform subjectTransform;

    private List<Vector3> desPoint = new List<Vector3>();

    private int currentIndex = 0;

    public FindDestination(Testing a) : base(a)
    {
        subjectTransform = context.transform;

        foreach (var point in context.points)
        {
            desPoint.Add(point.position);
        }
    }

    public override NodeStatus Tick()
    {
        if (currentIndex >= desPoint.Count)
            currentIndex = 0;

        context.nextPoint = desPoint[currentIndex];

        Debug.Log(desPoint[currentIndex]);

        currentIndex++;

        return NodeStatus.Sucess;
    }
}

public class MoveToDestination : Leaf<Testing>
{
    public MoveToDestination(Testing a) : base(a)
    {
    }

    public override NodeStatus Tick()
    {
        context.rb2d.velocity = context.transform.up * Time.deltaTime * 200;

        return NodeStatus.Sucess;
    }
}

public class LookAtDestination : Leaf<Testing>
{
    public LookAtDestination(Testing a) : base(a) { }

    public override NodeStatus Tick()
    {
        Vector2 lookAt = (context.nextPoint - context.transform.position).normalized;

        context.transform.up = lookAt;

        return NodeStatus.Sucess;
    }
}

public class ReachedDestination : Leaf<Testing>
{
    public ReachedDestination(Testing a) : base(a) { }

    public override NodeStatus Tick()
    {
        if (Vector2.Distance(context.transform.position, context.nextPoint) < 0.1f)
        {
            context.rb2d.MovePosition(context.nextPoint);
            return NodeStatus.Sucess;
        }

        return NodeStatus.Running;
    }
}