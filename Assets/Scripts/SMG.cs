using UnityEngine;

public class SMG : Weapon
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform  barrel;

    [SerializeField] private float FireRate = 10f;

    private float previousTime;

    private float intervalBetweenBullets;

    private void Awake()
    {
        intervalBetweenBullets = 1 / FireRate;
    }

    public override void Handle()
    {
        if (Time.time - previousTime > intervalBetweenBullets)
        {
            Fire();
            previousTime = Time.time;
        }
    }

    private void Fire()
    {
        GameObject newBullet = GameObject.Instantiate(bullet);

        newBullet.transform.position = barrel.position;
        newBullet.transform.right    = barrel.up;

        newBullet.GetComponent<Bullet>().GetDamageInfo().ownerName = transform.root.name;
        newBullet.GetComponent<Bullet>().GetDamageInfo().ownerTag  = transform.root.tag;

        // Temporarily turn off random spray pattern for deterministic physics
        //Vector3 shootDir = new Vector3(Random.Range(-0.5f, 0.5f), 0f, 0f) + barrel.up;

        Vector3 shootDir = new Vector3(0f, 0f, 0f) + barrel.up;

        newBullet.GetComponent<Rigidbody2D>().AddForce(shootDir * 20f, ForceMode2D.Impulse);
    }
}
