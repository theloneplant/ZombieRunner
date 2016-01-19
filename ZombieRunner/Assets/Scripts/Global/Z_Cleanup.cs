using UnityEngine;
using System.Collections;

public class Z_Cleanup : MonoBehaviour
{
    private Sprite sprite;              // Main method of removal, by checking whether the object is visible
    private BoxCollider2D box;          // Backup method, by checking its bounding box

	void Start ()
    {
        // Try to get the sprite, if that doesn't work use a bounding box
        if (GetComponent<SpriteRenderer>() != null)
            sprite = GetComponent<SpriteRenderer>().sprite;
        else
            box = GetComponent<BoxCollider2D>();

        // If neither work, don't clean it up (Add more options later)
	}
	
	void Update ()
    {
        // Calculate the right edge of the object, whether it's a sprite or bounding box
        float rightEdge = 0;
        if (sprite != null)
        {
            rightEdge = sprite.bounds.max.x * transform.localScale.x + transform.position.x;
            
        }
        else if (box != null)
        {
            rightEdge = box.size.x + box.transform.position.x;
        }

        // If the right edge of the object is off the left side of the screen
        if (rightEdge < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - 0.1f)
        {
            // Remove it
            Destroy(this.gameObject);
        }
	}
}
