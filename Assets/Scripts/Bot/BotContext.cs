using UnityEngine;
using System;
using System.Collections.Generic;

//TODO: better implementation
[Serializable]
public class BotContext
{
    public Transform[] patrolPoints { get; private set; }
    public Transform   transform    { get; private set; }
    public Transform   aim          { get; private set; }
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
    public Health Health        { get; private set; }

    public float MoveSpeed      { get; private set; }
    public float ChaseSpeed     { get; private set; }
    public float FleeSpeed      { get; private set; }

    public float AttackRange    { get; private set; }
    public float DetectionRange { get; private set; }
    public float ViewRange      { get; private set; }

    public HashSet<string> FriendTags   { get; private set; }
    public HashSet<string> EnemyTags    { get; private set; }
    public HashSet<string> ObstacleTags { get; private set; }

    public int LayerMask     { get; private set; }

    public BotStat(float MaxHP, 
                   float MoveSpeed, 
                   float ChaseSpeed, 
                   float FleeSpeed,
                   float AttackRange,
                   float DetectionRange,
                   float ViewRange,
                   int LayerMask,
                   IEnumerable<string> FriendTags,
                   IEnumerable<string> EnemyTags,
                   IEnumerable<string> ObstacleTags)
    {
        this.Health = new Health(MaxHP);

        this.MoveSpeed      = MoveSpeed;
        this.ChaseSpeed     = ChaseSpeed;
        this.FleeSpeed      = FleeSpeed;
        this.AttackRange    = AttackRange;
        this.DetectionRange = DetectionRange;
        this.ViewRange      = ViewRange;
        this.LayerMask      = LayerMask;


        this.FriendTags   = new HashSet<string>();
        this.EnemyTags    = new HashSet<string>();
        this.ObstacleTags = new HashSet<string>();

        foreach (string tag in FriendTags)
            this.FriendTags.Add(tag);

        foreach (string tag in EnemyTags)
            this.EnemyTags.Add(tag);

        foreach (string tag in ObstacleTags)
            this.ObstacleTags.Add(tag);
    }
}

[Serializable]
public class BotSense
{
    public bool IsUnderAttack;
}