﻿using UnityEngine;

public class TargetSighted : Leaf<BotContext>
{
    private Vector3 offset2;
    private Vector3 offset3;

    public TargetSighted(BotContext ctx) : base(ctx)
    {
        offset2 = new Vector3( 1f, 0f, 0f);
        offset3 = new Vector3(-1f, 0f, 0f);
    }

    public override NodeStatus Tick()
    {
        RaycastHit2D hit  = Physics2D.Raycast(context.aim.position, context.transform.up, context.stat.DetectionRange, context.stat.LayerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(context.aim.position, context.transform.up + offset2, context.stat.DetectionRange, context.stat.LayerMask);
        RaycastHit2D hit3 = Physics2D.Raycast(context.aim.position, context.transform.up + offset3, context.stat.DetectionRange, context.stat.LayerMask);


        if (hit.collider != null)
        {
            if (hit.collider.tag == context.stat.EnemyTag)
            {
                context.target = hit.collider.transform;
                return NodeStatus.Sucess;
            }
        }

        if (hit2.collider != null)
        {
            if (hit2.collider.tag == context.stat.EnemyTag)
            {
                context.target = hit2.collider.transform;
                return NodeStatus.Sucess;
            }
        }

        if (hit3.collider != null)
        {
            if (hit3.collider.tag == context.stat.EnemyTag)
            {
                context.target = hit3.collider.transform;
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
        //GameObject newBullet = GameObject.Instantiate(context.bullet);

        //newBullet.transform.position = context.aim.position;
        //newBullet.transform.right    = context.transform.up;

        ////TODO: implement Weapon interface
        //Vector3 shootDir = new Vector3(Random.Range(-1f, 1f), 0f, 0f) + context.transform.up;

        //newBullet.GetComponent<Rigidbody2D>().AddForce(shootDir * 20f, ForceMode2D.Impulse);

        context.weapon.Execute();

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
