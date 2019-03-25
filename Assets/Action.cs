using UnityEngine;
using System.Collections.Generic;

public class FindDestination : Leaf<AIContext>
{
    private Transform subjectTransform;

    private List<Vector3> desPoint = new List<Vector3>();

    private int currentIndex = 0;

    public FindDestination(AIContext ai) : base(ai)
    {
        subjectTransform = context.transform;

        foreach (var point in context.patrolPoints)
        {
            desPoint.Add(point.position);
        }
    }

    public override NodeStatus Tick()
    {
        Debug.Log("Find");

        if (currentIndex >= desPoint.Count)
            currentIndex = 0;

        context.nextPoint = desPoint[currentIndex];

        //Debug.Log(desPoint[currentIndex]);

        currentIndex++;

        Vector2 lookAt = (context.nextPoint - context.transform.position).normalized;

        context.lookAt = lookAt;

        return NodeStatus.Sucess;
    }
}

public class SteerAtDestination : Leaf<AIContext>
{
    public SteerAtDestination(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        context.transform.up = context.lookAt;

        Debug.Log("Steer");

        return NodeStatus.Sucess;
    }
}

public class MoveToDestination : Leaf<AIContext>
{
    public MoveToDestination(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Move");

        context.rb2d.velocity = context.transform.up * Time.fixedDeltaTime * context.moveSpeed;

        if (Vector2.Distance(context.transform.position, context.nextPoint) < 0.2f)
        {
            context.rb2d.MovePosition(context.nextPoint);
            return NodeStatus.Sucess;
        }

        if (context.sight.collision != null)
        {
            return NodeStatus.Sucess;
        }

        return NodeStatus.Running;
    }
}

public class TargetSighted : Leaf<AIContext>
{
    public TargetSighted(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Target Sighted");

        if (context.sight.collision != null)
        {
            if (context.sight.collision.tag == "Target")
            {
                context.target = context.sight.collision.transform;
                return NodeStatus.Sucess;
            }
        }

        return NodeStatus.Failure;
    }
}

public class ChaseTarget : Leaf<AIContext>
{
    public ChaseTarget(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Chase");

        if (context.sight.collision == null)
        {
            return NodeStatus.Failure;
        }

        context.rb2d.velocity = (context.target.position - context.transform.position).normalized * Time.fixedDeltaTime * context.chaseSpeed;

        return NodeStatus.Running;
    }
}