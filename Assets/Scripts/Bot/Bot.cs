using UnityEngine;
using UnityEngine.UI;

public enum Clan { Red, Blue }

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform   aim;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Image       healthBar;
    [SerializeField] private GameObject  bullet;
    [SerializeField] private Clan        clan;

    private INode root;
    private BotContext botCtx;
    private BotStat stat;

    private int layerMask = 0;

    private void Awake()
    {
        layerMask = layerMask |= (1 << LayerMask.NameToLayer("Bot"));
        layerMask = layerMask |= (1 << LayerMask.NameToLayer("WorldObject"));

        switch (clan)
        {
            case Clan.Red:  stat = new BotStat(MaxHP: 100f, 
                                               MoveSpeed: 100f, 
                                               ChaseSpeed: 150f, 
                                               AttackRange: 3f, 
                                               AlertRange: 5f,
                                               ViewRange: 2f,
                                               LayerMask: layerMask,
                                               FriendTag: "Red",
                                               EnemyTag: "Blue"); break;

            case Clan.Blue: stat = new BotStat(MaxHP: 100f,
                                               MoveSpeed: 100f,
                                               ChaseSpeed: 150f,
                                               AttackRange: 3f,
                                               AlertRange: 5f,
                                               ViewRange: 2f,
                                               LayerMask: layerMask,
                                               FriendTag: "Blue",
                                               EnemyTag: "Red"); break;
        }


        botCtx = new BotContext(stat,
                                GetComponent<Rigidbody2D>(),
                                GetComponent<Transform>(),
                                aim,
                                patrolPoints,
                                bullet);

        botCtx.stat.HpChanged += UpdateHealthBar;
        botCtx.stat.HpChanged += HPDepleted;
    }

    private void Start()
    {
        root = new Selector(
                     new Sequence(new TargetSighted(botCtx),
                                  new SteerAtTarget(botCtx),
                                  new Selector(new Sequence(new InAttackRange(botCtx),
                                                            new AttackTarget(botCtx),
                                                            new MoveWhileAttacking(botCtx)),
                                               new ChaseTarget(botCtx))),

                             new Selector(new Sequence(new Selector(new IfPathObstructed(botCtx),
                                                                    new IfReachDestination(botCtx)),
                                                       new FindNextDestination(botCtx),
                                                       new SteerAtDestination(botCtx)),

                                          new Sequence(new SteerAtDestination(botCtx),
                                                       new MoveToDestination(botCtx))));
    }

    private void FixedUpdate()
    {
        root.Tick();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            botCtx.stat.HP -= 5;
        }
    }

    private void UpdateHealthBar(System.Object sender, System.EventArgs eventArgs)
    {
        healthBar.fillAmount = botCtx.stat.HP / botCtx.stat.MaxHP;
    }

    private void HPDepleted(System.Object sender, System.EventArgs eventArgs)
    {
        if (botCtx.stat.HP <= 0)
            Destroy(this.gameObject);
    }
}
