using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Sequence tree;

    [HideInInspector]
    public Rigidbody2D rb2d;

    public Transform[] points;

    [HideInInspector]
    public Vector3 nextPoint;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        tree = new Sequence(new FindDestination(this),
                            new LookAtDestination(this),
                            new MoveToDestination(this),
                            new ReachedDestination(this));
    }

    private void FixedUpdate()
    {
        tree.Tick();
    }
}
