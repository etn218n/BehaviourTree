using UnityEngine;

public class SMG : Weapon
{
    [SerializeField] private Bullet bullet;
    public Transform barrel;

    public override void Execute()
    {
        Bullet newBullet = GameObject.Instantiate(bullet);

        newBullet.transform.position = barrel.position;
        newBullet.transform.right    = barrel.up;

        Vector3 shootDir = new Vector3(Random.Range(-0.5f, 0.5f), 0f, 0f) + barrel.up;

        newBullet.GetComponent<Rigidbody2D>().AddForce(shootDir * 20f, ForceMode2D.Impulse);
    }
}
