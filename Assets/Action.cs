using UnityEngine;
using System.Collections.Generic;

public class FindDestination : Leaf<AIContext>
{
    private int currentIndex = 0;

    public FindDestination(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Find Point");

        if (currentIndex >= context.patrolPoints.Length)
            currentIndex = 0;

        context.nextPoint = context.patrolPoints[currentIndex].position;

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

        Debug.Log("Steer Point");

        return NodeStatus.Sucess;
    }
}

public class MoveToDestination : Leaf<AIContext>
{
    public MoveToDestination(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Move to Point");

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

public class SteerAtTarget : Leaf<AIContext>
{
    public SteerAtTarget(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Steer Target");

        Vector2 lookAt = (context.target.position - context.transform.position).normalized;

        context.lookAt = lookAt;

        context.transform.up = context.lookAt;

        return NodeStatus.Sucess;
    }
}

public class ChaseTarget : Leaf<AIContext>
{
    public ChaseTarget(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Chase");

        context.rb2d.velocity = (context.target.position - context.transform.position).normalized * Time.fixedDeltaTime * context.chaseSpeed;

        return NodeStatus.Sucess;
    }
}