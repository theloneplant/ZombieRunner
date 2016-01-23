using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;           // Reference to player
    public float followThreshold;       // Percent of the screen from the bottom
    public float smoothing;             // How much to dampen motion

    private Camera cam;
    private float baseHeight;           // Starting location of the bottom of the screen
    private float playerHeight;
    private float initialTarget;

    void Start ()
    {
        cam = GetComponent<Camera>();
        float camHeight = cam.ViewportToWorldPoint(new Vector2(1, 1)).y - cam.ViewportToWorldPoint(new Vector2(0, 0)).y;
        baseHeight = transform.position.y;
        initialTarget = transform.position.y - camHeight / 2 + camHeight * followThreshold;
    }
	
	void Update ()
    {
        float camHeight = cam.ViewportToWorldPoint(new Vector2(1, 1)).y - cam.ViewportToWorldPoint(new Vector2(0, 0)).y;
        float targetLocation = transform.position.y - camHeight / 2 + camHeight * followThreshold;
        playerHeight = player.transform.position.y + player.GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

        if (playerHeight > initialTarget)
        {
            float newCamHeight = playerHeight + camHeight / 2 - camHeight * followThreshold;
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(transform.position.x, newCamHeight, transform.position.z), Time.deltaTime * smoothing);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(transform.position.x, baseHeight, transform.position.z), Time.deltaTime * smoothing * 2);
        }
	}
}
