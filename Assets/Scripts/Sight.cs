using UnityEngine;

public class Sight : MonoBehaviour
{
    public Collider2D collision { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision) { this.collision = collision; }
    private void OnTriggerExit2D (Collider2D collision) { this.collision = null;      }
}
