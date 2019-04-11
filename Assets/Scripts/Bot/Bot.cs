using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum Clan { None, Red, Blue }

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform   aim;
    [SerializeField] private Image       healthBar;
    [SerializeField] private Clan        clan;
    [SerializeField] private Weapon      weapon;

    [SerializeField] private SenseBehaviour senseBehaviour;

    private INode      root;
    private BotContext botCtx;
    private BotStat    stat;
    private BotSense   sense;

    private int layerMask = 0;

    private void Awake()
    {
        layerMask = layerMask |= (1 << LayerMask.NameToLayer("Node"));
        layerMask = layerMask |= (1 << LayerMask.NameToLayer("WorldObject"));

        sense = new BotSense();

        senseBehaviour.sense = sense;

        switch (clan)
        {
            case Clan.Red:  stat = new BotStat(MaxHP: 100f,
                                               MoveSpeed: 100f,
                                               ChaseSpeed: 150f,
                                               FleeSpeed: 175f,
                                               AttackRange: 3f,
                                               DetectionRange: 5f,
                                               ViewRange: 2f,
                                               LayerMask: layerMask,
                                               FriendTags:   new List<string>  { "Red" },
                                               EnemyTags:    new List<string>  { "Blue"},
                                               ObstacleTags: new List<string>  { "WorldObject", "Red" });
                                               break;

            case Clan.Blue: stat = new BotStat(MaxHP: 100f,
                                               MoveSpeed: 100f,
                                               ChaseSpeed: 150f,
                                               FleeSpeed: 175f,
                                               AttackRange: 3f,
                                               DetectionRange: 5f,
                                               ViewRange: 2f,
                                               LayerMask: layerMask,
                                               FriendTags:   new List<string> { "Blue" },
                                               EnemyTags:    new List<string> { "Red"  },
                                               ObstacleTags: new List<string> { "WorldObject", "Blue" });
                                               break;
        }

        weapon = GameObject.Instantiate(weapon, aim.transform, false);

        botCtx = new BotContext(stat,
                                sense,
                                GetComponent<Rigidbody2D>(),
                                GetComponent<Transform>(),
                                aim,
                                patrolPoints,
                                weapon);

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

                                  new Selector(//new Sequence(new IfUnderAttack(botCtx),
                                               //             new Flee(botCtx)),

                                               new Sequence(new SteerAtDestination(botCtx),
                                                            new MoveToDestination(botCtx)))));
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
            botCtx.sense.IsUnderAttack = true;
        }
    }

    private void UpdateHealthBar(System.Object sender, System.EventArgs eventArgs)
    {
        healthBar.fillAmount = botCtx.stat.HP / botCtx.stat.MaxHP;
    }

    private void HPDepleted(System.Object sender, System.EventArgs eventArgs)
    {
        if (botCtx.stat.HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
