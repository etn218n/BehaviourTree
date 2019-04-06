using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0f, 100f)] private float damage;

    void Start()
    {
        StartCoroutine(Timer(2f));
    }

    private IEnumerator Timer(float interval)
    {
        yield return new WaitForSeconds(interval);

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Timer(0.1f));
    }
}
