using UnityEngine;

public class FindNextDestination : Leaf<AIContext>
{
    private int currentIndex = 0;

    public FindNextDestination(AIContext ai) : base(ai)
    {
        context.nextPoint = context.patrolPoints[0];
    }

    public override NodeStatus Tick()
    {
        Debug.Log("Find Point");

        if (currentIndex >= context.patrolPoints.Length)
            currentIndex = 0;

        context.nextPoint = context.patrolPoints[currentIndex];

        currentIndex++;

        return NodeStatus.Sucess;
    }
}

public class IfReachDestination : Leaf<AIContext>
{
    public IfReachDestination(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Reached Point");

        if (Vector2.Distance(context.transform.position, context.nextPoint.position) < 0.2f)
        {
            context.rb2d.MovePosition(context.nextPoint.position);
            return NodeStatus.Sucess;
        }

        return NodeStatus.Failure;
    }
}

public class SteerAtDestination : Leaf<AIContext>
{
    public SteerAtDestination(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Steer Point");

        Vector2 lookAt = (context.nextPoint.position - context.transform.position).normalized;

        context.transform.up = lookAt;

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

        return NodeStatus.Sucess;
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

        context.transform.up = lookAt;

        return NodeStatus.Sucess;
    }
}

public class ChaseTarget : Leaf<AIContext>
{
    public ChaseTarget(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        Debug.Log("Chase Target");

        context.rb2d.velocity = context.transform.up * Time.fixedDeltaTime * context.chaseSpeed;

        return NodeStatus.Sucess;
    }
}