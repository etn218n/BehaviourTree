using UnityEngine;

public class SMG : Weapon
{
    [SerializeField] private Bullet    bullet;
    [SerializeField] private Transform barrel;

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
        Bullet newBullet = GameObject.Instantiate(bullet);

        newBullet.transform.position = barrel.position;
        newBullet.transform.right    = barrel.up;

        newBullet.GetDamageInfo().ownerName = transform.root.name;
        newBullet.GetDamageInfo().ownerTag  = transform.root.tag;

        Vector3 shootDir = new Vector3(Random.Range(-0.5f, 0.5f), 0f, 0f) + barrel.up;

        newBullet.GetComponent<Rigidbody2D>().AddForce(shootDir * 20f, ForceMode2D.Impulse);
    }
}
