using UnityEngine;

public class TargetSighted : Leaf<BotContext>
{
    public TargetSighted(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        RaycastHit2D hit = Physics2D.Raycast(context.aim.position, context.transform.up, context.stat.AlertRange, context.stat.LayerMask);

        if (hit.collider != null)
        {
            if (hit.collider.tag == context.stat.EnemyTag)
            {
                context.target = hit.collider.transform;
                return NodeStatus.Sucess;
            }
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
        GameObject newBullet = GameObject.Instantiate(context.bullet);

        newBullet.transform.position = context.aim.position;
        newBullet.transform.right    = context.transform.up;

        //TODO: implement Weapon interface
        Vector3 shootDir = new Vector3(Random.Range(-1f, 1f), 0f, 0f) + context.transform.up;

        newBullet.GetComponent<Rigidbody2D>().AddForce(shootDir * 20f, ForceMode2D.Impulse);

        return NodeStatus.Sucess;
    }
}

public class MoveWhileAttacking : Leaf<BotContext>
{
    public MoveWhileAttacking(BotContext ctx) : base(ctx) { }

    public override NodeStatus Tick()
    {
        int randomValue = Random.Range(0, 2);

        if (randomValue < 1)
            context.rb2d.velocity =  context.transform.right * 100f * Time.fixedDeltaTime;
        else
            context.rb2d.velocity = -context.transform.right * 100f * Time.fixedDeltaTime;

        return NodeStatus.Sucess;
    }
}
