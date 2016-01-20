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
        Vector2 bottomBack = new Vector2(transform.position.x, transform.position.y);
		Vector2 bottomMid = new Vector2(transform.position.x + hitBox.size.x / 2, transform.position.y);
		Vector2 bottomFront = new Vector2(transform.position.x + hitBox.size.x, transform.position.y);

		// Check for back, mid, and front collision
		checkHit(Physics2D.Raycast(bottomBack, Vector2.down, Mathf.Infinity, layerMask));
		checkHit(Physics2D.Raycast(bottomMid, Vector2.down, Mathf.Infinity, layerMask));
		checkHit(Physics2D.Raycast(bottomFront, Vector2.down, Mathf.Infinity, layerMask));

        // Update previous position
        prevPosition = rb.position;
    }

	private void checkHit(RaycastHit2D hit)
	{
		if (hit.collider != null)
		{
			// Use previous velocity to see if the object will pass through a collider
			float yDelta = rb.position.y - prevPosition.y;
			if (hit.distance < Mathf.Abs(yDelta) && yDelta < 0 && hit.distance > 0)
			{
				// There will be a collision next frame, so set the object on top of the collider
				rb.position = new Vector2(hit.point.x, hit.point.y - 0.001f);
				rb.velocity = new Vector2(rb.velocity.x, 0);
			}
		}
	}
}