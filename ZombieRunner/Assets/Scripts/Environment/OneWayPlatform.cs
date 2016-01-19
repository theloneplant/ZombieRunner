using UnityEngine;
using System.Collections;

public class OneWayPlatform : MonoBehaviour
{
    public BoxCollider2D player;

    private BoxCollider2D hitBox;

	void Start ()
    {
        hitBox = GetComponent<BoxCollider2D>();
	}
	
	void FixedUpdate ()
    {
        // Ignores player and building collision if the player is below buildings
        float top = transform.position.y + hitBox.offset.y * transform.localScale.y;
        hitBox.enabled = player.transform.position.y >= top;
    }
}
