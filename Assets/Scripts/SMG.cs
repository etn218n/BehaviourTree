﻿using UnityEngine;

public class SMG : Weapon
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform  barrel;

    [SerializeField] private float FireRate = 10f;

    private float lastFireTime;
    private float intervalBetweenBullets;

    private void Awake()
    {
        intervalBetweenBullets = 1 / FireRate;
    }

    public override void Handle()
    {
        if (Time.time - lastFireTime > intervalBetweenBullets)
        {
            Fire();

            lastFireTime = Time.time;
        }
    }

    private void Fire()
    {
        GameObject newBullet = GameObject.Instantiate(bullet);

        newBullet.transform.position = barrel.position;
        newBullet.transform.right    = barrel.up;

        newBullet.GetComponent<Bullet>().GetDamageInfo().ownerName = transform.root.name;
        newBullet.GetComponent<Bullet>().GetDamageInfo().ownerTag  = transform.root.tag;

        // Random spray pattern
        Vector3 shootDir = new Vector3(Random.Range(-0.4f, 0.4f), 0f, 0f) + barrel.up;

        newBullet.GetComponent<Rigidbody2D>().AddForce(shootDir * 20f, ForceMode2D.Impulse);
    }
}
