using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IAffectedByObstacle
{
    public float jumpVelocity = 15;         // How fast the player will jump
    public float jumpTime = 0.25f;           // How long this velocity will hold until freefall
    public float startRunSpeed = 2;         // Initial player speed

    private Rigidbody2D rb;                 // Player's rigidbody
    private float startTime;                // Time the player started jumping
    private bool onGround;                  // Whether the player is able to jump yet
    private bool buttonUp;                  // Prevents the player from repeatedly pressing jump while the timer is going

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        onGround = false;
        buttonUp = false;
        Z_Globals.RunSpeed = startRunSpeed;
    }
	
	void FixedUpdate ()
    {
        if (Input.GetButtonUp("Jump"))
        {
            // Prevent the player from jumping again until they land
            buttonUp = true;
        }
        else if (Input.GetButton("Jump") && rb.velocity.y >= 0)
        {
            if (onGround)
            {
                // Reset flags and timer for the new jump
                onGround = false;
                buttonUp = false;
                startTime = Time.time;
            }

            // Keep the current upward velocity as long as the player holds jump and the timer isn't finished
            if (Time.time - startTime <= jumpTime && !buttonUp)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            }
        }
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Ground")
        {
            // The player has landed and is able to jump again
            onGround = true;
        }
    }

    // TODO: What happens to the player when they touch an obstacle?
    public void TouchedObstacle(float spMod)
    { 
    
    }
}
