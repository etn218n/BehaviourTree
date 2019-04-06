using UnityEngine;

public class TargetSighted : Leaf<BotContext>
{
    private int layerMask = 0;

    public TargetSighted(BotContext ctx) : base(ctx)
    {
        layerMask |= (1 << LayerMask.NameToLayer("Bot"));
    }

    public override NodeStatus Tick()
    {
        RaycastHit2D hit = Physics2D.Raycast(context.aim.position, context.transform.up, context.stat.ViewRange, layerMask);

        if (hit.collider != null)
        {
            context.target = hit.collider.transform;
            return NodeStatus.Sucess;
        } 

        return NodeStatus.Failure;
    }
}

public class SteerAtTarget : Leaf<BotContext>
{
    public SteerAtTarget(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        if (context.target == null)
            return NodeStatus.Failure;

        Vector2 lookAt = (context.target.position - context.transform.position).normalized;

        context.transform.up = lookAt;

        return NodeStatus.Sucess;
    }
}

public class ChaseTarget : Leaf<BotContext>
{
    public ChaseTarget(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        context.rb2d.velocity = context.transform.up * Time.fixedDeltaTime * context.stat.ChaseSpeed;

        return NodeStatus.Sucess;
    }
}

public class InAttackRange : Leaf<BotContext>
{
    public InAttackRange(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        if (context.target == null)
            return NodeStatus.Failure;

        if (Vector3.Distance(context.target.position, context.transform.position) < context.stat.AttackRange)
        {
            return NodeStatus.Sucess;
        }

        return NodeStatus.Failure;
    }
}

public class AttackTarget : Leaf<BotContext>
{
    public AttackTarget(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        context.rb2d.velocity = Vector2.zero;

        GameObject newBullet = GameObject.Instantiate(context.bullet);
        newBullet.transform.position = context.aim.position;
        newBullet.transform.right = context.transform.up;
        newBullet.GetComponent<Rigidbody2D>().AddForce(context.transform.up * 20f, ForceMode2D.Impulse);

        return NodeStatus.Sucess;
    }
}
