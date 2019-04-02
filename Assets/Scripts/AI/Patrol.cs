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
        context.rb2d.velocity = context.transform.up * Time.fixedDeltaTime * context.moveSpeed;

        if (context.sight.collision != null)
        {
            if (context.sight.collision.tag == "WorldObject")
            {
                Debug.Log("Hit");
                return NodeStatus.Failure;
            }
        }

        return NodeStatus.Sucess;
    }
}

public class IfPathObstructed : Leaf<AIContext>
{
    public IfPathObstructed(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        if (context.sight.collision != null)
        {
            if (context.sight.collision.tag == "WorldObject")
                return NodeStatus.Sucess;
        }

        return NodeStatus.Failure;
    }
}