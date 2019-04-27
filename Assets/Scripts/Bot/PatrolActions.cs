using UnityEngine;

public class FindNextDestination : Leaf<BotContext>
{
    public FindNextDestination(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        context.nextPoint = context.patrolPoints.GetNext();

        return NodeStatus.Sucess;
    }
}

public class IfReachDestination : Leaf<BotContext>
{
    public IfReachDestination(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        if (Vector2.Distance(context.transform.position, context.nextPoint) < 0.2f)
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
        Vector2 lookAt = (context.nextPoint - context.transform.position).normalized;

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
    public IfPathObstructed(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        RaycastHit2D hit = Physics2D.Raycast(context.aim.position, context.transform.up, context.stat.ViewRange, context.stat.LayerMask);

        //RaycastHit2D hit = Physics2D.CircleCast(context.aim.position, 0.2f, context.aim.up, context.stat.ViewRange, context.stat.LayerMask);

        if (hit.collider != null)
        {
            foreach (string tag in context.stat.ObstacleTags)
            {
                if (hit.collider.tag == tag)
                {
                    return NodeStatus.Sucess;
                }
            }
        }

        return NodeStatus.Failure;
    }
}