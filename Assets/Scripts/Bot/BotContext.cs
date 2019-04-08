using UnityEngine;
using System;

//TODO: better implementation
[Serializable]
public class BotContext
{
    public Transform[] patrolPoints { get; private set; }
    public Transform transform      { get; private set; }
    public Transform aim            { get; private set; }
    public Rigidbody2D rb2d         { get; private set; }

    public BotStat  stat            { get; private set; }
    public BotSense sense           { get; private set; }

    public Transform nextPoint { get; set; }
    public Transform target    { get; set; }
    public Weapon    weapon    { get; set; }

    public BotContext(BotStat stat,
                      BotSense sense,
                      Rigidbody2D rb2d,
                      Transform transform,
                      Transform aim,
                      Transform[] patrolPoints,
                      Weapon weapon)
    {
        this.stat         = stat;
        this.sense        = sense;
        this.rb2d         = rb2d;
        this.transform    = transform;
        this.aim          = aim;
        this.patrolPoints = patrolPoints;
        this.weapon       = weapon;
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

    public float MaxHP          { get; private set; }

    public float MoveSpeed      { get; private set; }
    public float ChaseSpeed     { get; private set; }
    public float FleeSpeed      { get; private set; }

    public float AttackRange    { get; private set; }
    public float DetectionRange { get; private set; }
    public float ViewRange      { get; private set; }

    public string FriendTag  { get; private set; }
    public string EnemyTag   { get; private set; }

    public int LayerMask     { get; private set; }

    public BotStat(float MaxHP, 
                   float MoveSpeed, 
                   float ChaseSpeed, 
                   float FleeSpeed,
                   float AttackRange,
                   float DetectionRange,
                   float ViewRange,
                   int LayerMask,
                   string FriendTag,
                   string EnemyTag)
    {
        this.MaxHP          = MaxHP;
        this.hp             = MaxHP;
        this.MoveSpeed      = MoveSpeed;
        this.ChaseSpeed     = ChaseSpeed;
        this.FleeSpeed      = FleeSpeed;
        this.AttackRange    = AttackRange;
        this.DetectionRange = DetectionRange;
        this.ViewRange      = ViewRange;
        this.LayerMask      = LayerMask;
        this.FriendTag      = FriendTag;
        this.EnemyTag       = EnemyTag;
    }
}

[Serializable]
public class BotSense
{
    public bool IsUnderAttack;
}