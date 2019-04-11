using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform aim;
    [SerializeField] private Image healthBar;
    [SerializeField] private Weapon weapon;

    [SerializeField] private SenseBehaviour senseBehaviour;

    private float MaxHP = 100f;
    private float HP    = 100f;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        Cursor.visible = false;
        weapon = GameObject.Instantiate(weapon, aim.transform, false);

        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        transform.up = mousePos - transform.position;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb2d.velocity = new Vector2(x, y).normalized * 100f * Time.fixedDeltaTime;


        if (Input.GetMouseButtonDown(0))
        {
            weapon.Handle();
        }
    }

    private void FixedUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            HP -= 5f;

            UpdateHealthBar();
            HPDepleted();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = HP / MaxHP;
    }

    private void HPDepleted()
    {
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
