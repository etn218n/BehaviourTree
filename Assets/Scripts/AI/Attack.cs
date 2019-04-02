using UnityEngine;

public class TargetSighted : Leaf<AIContext>
{
    public TargetSighted(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        context.sight.transform.localScale = new Vector2(context.sightScaleX, context.sightScaleY);

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
        context.rb2d.velocity = context.transform.up * Time.fixedDeltaTime * context.chaseSpeed;

        return NodeStatus.Sucess;
    }
}

public class InAttackRange : Leaf<AIContext>
{
    public InAttackRange(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        if (Vector3.Distance(context.target.position, context.transform.position) < context.attackRange)
        {
            return NodeStatus.Sucess;
        }

        return NodeStatus.Failure;
    }
}

public class AttackTarget : Leaf<AIContext>
{
    public AttackTarget(AIContext ai) : base(ai) { }

    public override NodeStatus Tick()
    {
        context.rb2d.velocity = Vector2.zero;

        Object.Destroy(context.target.gameObject);

        return NodeStatus.Sucess;
    }
}
