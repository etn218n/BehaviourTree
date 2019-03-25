using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Selector tree;

    private Rigidbody2D rb2d;
    private Transform transform;

    public Transform[] patrolPoints;

    [SerializeField] private Sight sight;

    [SerializeField] private AIContext ai;

    private void Awake()
    {
        rb2d      = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();

        ai = new AIContext(rb2d, transform, sight, patrolPoints);
    }

    private void Start()
    {
        tree = new Selector(

            new Sequence(new TargetSighted(ai),
                         new ChaseTarget(ai)),

            new Sequence(new FindDestination(ai),
                         new SteerAtDestination(ai),
                         new MoveToDestination(ai)));
    }

    private void FixedUpdate()
    {
        tree.Tick();
    }
}
