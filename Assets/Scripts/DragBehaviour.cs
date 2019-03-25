using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DragBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDrag()
    {
        Vector2 dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rb2d.MovePosition(dragPosition);
    }
}
