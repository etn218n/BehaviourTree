using System.Collections;
using UnityEngine;

// Object pooling system awaits :)
public class Bullet : MonoBehaviour
{
    public readonly float damage = 50f;
    

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
        StartCoroutine(Timer(0.2f));
    }
}
