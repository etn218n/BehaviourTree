using UnityEngine;

public class AI : MonoBehaviour
{
    private Selector tree;

    public Transform[] patrolPoints;

    [SerializeField] private Sight sight;
    [SerializeField] private AIContext ai;

    private void Awake()
    {
        ai = new AIContext(GetComponent<Rigidbody2D>(), 
                           transform, 
                           sight, 
                           patrolPoints);
    }

    private void Start()
    {
        tree = new Selector(
                     new Sequence(new TargetSighted(ai),
                                  new SteerAtTarget(ai),
                                  new ChaseTarget(ai)),

                     new Selector(new Sequence(new IfReachDestination(ai),
                                               new FindNextDestination(ai)),

                                  new Sequence(new SteerAtDestination(ai),
                                               new MoveToDestination(ai))));
    }

    private void FixedUpdate()
    {
        tree.Tick();
    }
}
