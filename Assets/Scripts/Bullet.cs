using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// Object pooling system awaits :)
public class Bullet : NetworkBehaviour, IDamage
{
    private DamageInfo damageInfo = new DamageInfo(10f);

    void Start()
    {
        StartCoroutine(Timer(5f));
    }

    protected IEnumerator Timer(float existenceDuration)
    {
        yield return new WaitForSeconds(existenceDuration);

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            return;

        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.DamagedBy(this);
        }

        //StartCoroutine(Timer(0.2f));
    }

    public DamageInfo GetDamageInfo() { return this.damageInfo; }
}
