using UnityEngine;
using System;

[Serializable]
public class AIContext
{
    public Transform[] patrolPoints { get; private set; }
    public Transform   transform    { get; private set; }
    public Rigidbody2D rb2d         { get; private set; }
    public Sight       sight        { get; private set; }

    public Transform nextPoint { get; set; }
    public Transform target    { get; set; }

    [Range(0f, 200f)] public float moveSpeed   = 100f;
    [Range(0f, 200f)] public float chaseSpeed  = 150f;
    [Range(0f, 5f)  ] public float attackRange = 1.2f;

    [Range(0f, 5f)  ] public float sightScaleX = 3f;
    [Range(0f, 5f)  ] public float sightScaleY = 3f;

    public AIContext(Rigidbody2D rb2d, 
                     Transform transform, 
                     Sight sight, 
                     Transform[] patrolPoints)
    {
        this.rb2d         = rb2d;
        this.sight        = sight;
        this.transform    = transform;
        this.patrolPoints = patrolPoints;
    }
}
