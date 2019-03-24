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

        context.nexPoint = desPoint[currentIndex];

        Debug.Log(desPoint[currentIndex]);

        currentIndex++;

        return NodeStatus.Sucess;
    }
}

public class MoveTo : Leaf<Testing>
{
    public MoveTo(Testing a) : base(a)
    {
    }

    public override NodeStatus Tick()
    {
        context.rb2d.velocity = (context.nexPoint - context.transform.position).normalized * Time.deltaTime * 200;

        if (Vector2.Distance(context.transform.position, context.nexPoint) < 0.1f)
        {
            context.rb2d.MovePosition(context.nexPoint);
            return NodeStatus.Sucess;
        }

        return NodeStatus.Running;
    }
}