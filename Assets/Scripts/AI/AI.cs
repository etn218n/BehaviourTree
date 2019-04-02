using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private Sight sight;
    [SerializeField] private AIContext aiCtx;
    [SerializeField] private GameObject bullet;

    private INode root;

    public Transform[] patrolPoints;

    private void Awake()
    {
        aiCtx = new AIContext(GetComponent<Rigidbody2D>(), 
                              transform, 
                              sight, 
                              patrolPoints);
    }

    private void Start()
    {
        root = new Selector(
                     new Sequence(new TargetSighted(aiCtx),
                                  new SteerAtTarget(aiCtx),
                                  new Selector(new Sequence(new InAttackRange(aiCtx),
                                                            new AttackTarget(aiCtx)),
                                               new ChaseTarget(aiCtx))),

                     new Selector(new Sequence(new Selector(new IfPathObstructed(aiCtx),
                                                            new IfReachDestination(aiCtx)),
                                               new FindNextDestination(aiCtx),
                                               new SteerAtDestination(aiCtx)),

                                  new Sequence(new SteerAtDestination(aiCtx),
                                               new MoveToDestination(aiCtx))));
    }

    private void FixedUpdate()
    {
        root.Tick();
    }
}
