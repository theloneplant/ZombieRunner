using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour
{
    public float waveHeight;
    public float moveSpeed;

    private float startHeight;
    private float startTime;

	// Use this for initialization
	void Start ()
    {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, 
            startHeight + waveHeight * Mathf.Sin(startTime + Time.time * moveSpeed), transform.position.z);
	}

    public void initStartHeight()
    {
        startHeight = transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            Destroy(gameObject);
    }
}
