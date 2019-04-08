using UnityEngine;

public class IfUnderAttack : Leaf<BotContext>
{
    public IfUnderAttack(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        if (context.sense.IsUnderAttack)
            return NodeStatus.Sucess;

        return NodeStatus.Failure;
    }
}

public class Flee : Leaf<BotContext>
{
    private float timer = 0;

    public Flee(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        if (timer < 2f)
        {
            timer += Time.fixedDeltaTime;
            context.rb2d.velocity = context.transform.up * context.stat.FleeSpeed * Time.fixedDeltaTime;
            return NodeStatus.Running;
        }
        else
        {
            timer = 0f;
            context.sense.IsUnderAttack = false;
            return NodeStatus.Sucess;
        }
    }
}
