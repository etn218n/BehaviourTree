using UnityEngine;

public class Sight : MonoBehaviour
{
    // TODO: Need better implementation
    public Collider2D collision { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
            this.collision = collision;
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        this.collision = null;
    }
}
