using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AIContext
{
    public Transform[] patrolPoints { get; private set; }
    public Transform   transform    { get; private set; }
    public Rigidbody2D rb2d         { get; private set; }
    public Sight       sight        { get; private set; }

    public Vector3 nextPoint { get; set; }
    public Vector3 lookAt    { get; set; }

    [Range(0, 200)] public float moveSpeed  = 50f;
    [Range(0, 200)] public float steerSpeed = 50f;

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
