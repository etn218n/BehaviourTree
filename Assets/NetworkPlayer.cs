using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private Transform aim;
    [SerializeField] private Weapon weapon;

    [SerializeField] private SenseBehaviour senseBehaviour;

    [SyncVar] private Vector3 syncPosition;
    [SyncVar] private Vector3 syncRotation;

    private float lerpRate = 11f;

    private Health health = new Health(100f);

    private Rigidbody2D rb2d;

    private void Awake()
    {
        weapon = GameObject.Instantiate(weapon, aim.transform, false);

        rb2d = GetComponent<Rigidbody2D>();

        //health.OnDepleted += (System.Object sender, System.EventArgs eventArgs) => Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        transform.up = mousePos - transform.position;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 newPos = transform.position + new Vector3(x, y, 0f).normalized * 5f * Time.fixedDeltaTime;

        rb2d.MovePosition(newPos);

        CmdSendNewState(newPos, transform.up);

        if (Input.GetMouseButton(0))
        {
            weapon.Handle();

            CmdFire();
        }
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            rb2d.MovePosition(Vector3.Lerp(rb2d.position, syncPosition, Time.fixedDeltaTime * lerpRate));
        }
    }

    [Command]
    private void CmdSendNewState(Vector3 newPos, Vector3 up)
    {
        rb2d.MovePosition(newPos);

        syncPosition = newPos;
        syncRotation = up;

        RpcUpdateNewPos();
    }

    [ClientRpc]
    private void RpcUpdateNewPos()
    {
        if (isLocalPlayer)
            return;

        rb2d.MovePosition(syncPosition);

        transform.up = syncRotation;
    }

    [Command]
    private void CmdFire()
    {
        weapon.Handle();

        RpcFire();
    }

    [ClientRpc]
    private void RpcFire()
    {
        weapon.Handle();
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        rb2d.velocity = Vector2.zero;
    //    }
    //}

    //public Health GetHealth()
    //{
    //    return this.health;
    //}

    //public void DamagedBy(IDamage dealer)
    //{
    //    health.DecreaseBy(dealer.GetDamageInfo().damage);
    //}
}
