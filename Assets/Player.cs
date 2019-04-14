using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour, IHealthGauge
{
    [SerializeField] private Transform aim;
    [SerializeField] private Weapon weapon;

    [SerializeField] private SenseBehaviour senseBehaviour;

    private Health health = new Health(100f);

    private Rigidbody2D rb2d;

    private void Awake()
    {
        weapon = GameObject.Instantiate(weapon, aim.transform, false);

        rb2d = GetComponent<Rigidbody2D>();

        health.OnDepleted += (System.Object sender, System.EventArgs eventArgs) =>  Destroy(this.gameObject);
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        transform.up = mousePos - transform.position;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb2d.velocity = new Vector2(x, y).normalized * 100f * Time.fixedDeltaTime;

        if (Input.GetMouseButton(0))
        {
            weapon.Handle();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health.DecreaseBy(collision.gameObject.GetComponent<Bullet>().damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    public Health GetHealth()
    {
        return this.health;
    }
}
