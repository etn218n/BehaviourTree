﻿using UnityEngine;
using UnityEngine.UI;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform   aim;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Image       healthBar;
    [SerializeField] private GameObject  bullet;

    private INode root;
    private BotContext botCtx;
    private BotStat stat;

    private void Awake()
    {
        stat = new BotStat(MaxHP: 100f, MoveSpeed: 100f, ChaseSpeed: 150f, AttackRange: 2f, ViewRange: 4f);

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
                                                            new AttackTarget(botCtx)),
                                               new ChaseTarget(botCtx))),

                             new Selector(new Sequence(new Selector(new IfPathObstructed(botCtx),
                                                                    new IfReachDestination(botCtx)),
                                                       new FindNextDestination(botCtx),
                                                       new SteerAtDestination(botCtx)),

                                          new Sequence(new SteerAtDestination(botCtx),
                                                       new MoveToDestination(botCtx))));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            stat.HP -= 5f;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            stat.HP += 5f;
        }
    }

    private void FixedUpdate()
    {
        root.Tick();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        stat.HP -= 5f;
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
