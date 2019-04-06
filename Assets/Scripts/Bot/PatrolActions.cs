using UnityEngine;

public class FindNextDestination : Leaf<BotContext>
{
    private int currentIndex = 0;

    public FindNextDestination(BotContext ctx) : base(ctx)
    {
        int index = Random.Range(0, context.patrolPoints.Length - 1);
        context.nextPoint = context.patrolPoints[index];
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

public class IfReachDestination : Leaf<BotContext>
{
    public IfReachDestination(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        if (Vector2.Distance(context.transform.position, context.nextPoint.position) < 0.2f)
        {
            return NodeStatus.Sucess;
        }

        return NodeStatus.Failure;
    }
}

public class SteerAtDestination : Leaf<BotContext>
{
    public SteerAtDestination(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        Vector2 lookAt = (context.nextPoint.position - context.transform.position).normalized;

        context.transform.up = lookAt;

        return NodeStatus.Sucess;
    }
}

public class MoveToDestination : Leaf<BotContext>
{
    public MoveToDestination(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        context.rb2d.velocity = context.transform.up * Time.fixedDeltaTime * context.stat.MoveSpeed;

        return NodeStatus.Sucess;
    }
}

public class IfPathObstructed : Leaf<BotContext>
{
    private int layerMask = 0;

    public IfPathObstructed(BotContext ctx) : base(ctx)
    {
        layerMask |= (1 << LayerMask.NameToLayer("WorldObject"));
    }

    public override NodeStatus Tick()
    {
        RaycastHit2D hit = Physics2D.Raycast(context.aim.position, context.transform.up, context.stat.ViewRange, layerMask);

        if (hit.collider != null)
        {
            return NodeStatus.Sucess;
        }

        return NodeStatus.Failure;
    }
}