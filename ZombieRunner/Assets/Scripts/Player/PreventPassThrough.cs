using UnityEngine;
using System.Collections;

public class PreventPassThrough : MonoBehaviour
{
    public LayerMask layerMask;

    private BoxCollider2D hitBox;
    private Rigidbody2D rb;
    private Vector2 prevPosition;
    
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>(); // Add more colliders in the future
        rb = GetComponent<Rigidbody2D>();
        prevPosition = rb.position;
    }

    void FixedUpdate()
    {
        Vector2 bottomFront = new Vector2(transform.position.x + hitBox.size.x / 2, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(bottomFront, rb.velocity.normalized, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            // Use previous velocity to see if the object will pass through a collider
            float yDelta = rb.position.y - prevPosition.y;
            if (hit.distance < Mathf.Abs(yDelta) && yDelta < 0 && hit.distance > 0)
            {
                // There will be a collision next frame, so set the object on top of the collider
                rb.position = new Vector2(hit.point.x, hit.point.y);
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        // Update previous position
        prevPosition = rb.position;
    }
}