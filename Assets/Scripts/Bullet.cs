using System.Collections;
using UnityEngine;

// Object pooling system awaits :)
public class Bullet : MonoBehaviour, IDamage
{
    private DamageInfo damageInfo = new DamageInfo(10f);

    void Start()
    {
        StartCoroutine(Timer(2f));
    }

    private IEnumerator Timer(float existenceDuration)
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

        StartCoroutine(Timer(0.2f));
    }

    public DamageInfo GetDamageInfo() { return this.damageInfo; }
}
