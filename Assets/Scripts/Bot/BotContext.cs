using UnityEngine;
using System;

[Serializable]
public class BotContext
{
    public Transform[] patrolPoints { get; private set; }
    public Transform transform      { get; private set; }
    public Transform aim            { get; private set; }
    public Rigidbody2D rb2d         { get; private set; }
    public BotStat stat             { get; private set; }

    public GameObject bullet { get; private set; }

    public Transform nextPoint { get; set; }
    public Transform target    { get; set; }

    public BotContext(BotStat stat,
                      Rigidbody2D rb2d,
                      Transform transform,
                      Transform aim,
                      Transform[] patrolPoints,
                      GameObject bullet)
    {
        this.stat         = stat;
        this.rb2d         = rb2d;
        this.transform    = transform;
        this.aim          = aim;
        this.patrolPoints = patrolPoints;
        this.bullet       = bullet;
    }
}

[Serializable]
public class BotStat
{
    public EventHandler HpChanged;

    private float hp;
    public float HP
    {
        get => this.hp;

        set
        {
            this.hp = value;

            HpChanged?.Invoke(this, null);
        }
    }

    public float MaxHP       { get; private set; }
    public float MoveSpeed   { get; private set; }
    public float ChaseSpeed  { get; private set; }
    public float AttackRange { get; private set; }
    public float ViewRange   { get; private set; }

    public string FriendTag  { get; private set; }
    public string EnemyTag   { get; private set; }

    public int LayerMask     { get; private set; }

    public BotStat(float MaxHP, 
                   float MoveSpeed, 
                   float ChaseSpeed, 
                   float AttackRange, 
                   float ViewRange,
                   int LayerMask,
                   string FriendTag,
                   string EnemyTag)
    {
        this.MaxHP       = MaxHP;
        this.hp          = MaxHP;
        this.MoveSpeed   = MoveSpeed;
        this.ChaseSpeed  = ChaseSpeed;
        this.AttackRange = AttackRange;
        this.ViewRange   = ViewRange;
        this.LayerMask   = LayerMask;
        this.FriendTag   = FriendTag;
        this.EnemyTag    = EnemyTag;
    }
}