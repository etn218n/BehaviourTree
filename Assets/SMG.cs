using UnityEngine;

public class SMG : Weapon
{
    [SerializeField] private Bullet    bullet;
    [SerializeField] private Transform barrel;

    [SerializeField] private float FireRate = 10f;

    private float previousTime;

    private float interval;

    private void Awake()
    {
        interval = 1 / FireRate;
    }

    public override void Handle()
    {
        if (Time.time - previousTime > interval)
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

        Vector3 shootDir = new Vector3(Random.Range(-0.7f, 0.7f), 0f, 0f) + barrel.up;

        newBullet.GetComponent<Rigidbody2D>().AddForce(shootDir * 20f, ForceMode2D.Impulse);
    }
}
