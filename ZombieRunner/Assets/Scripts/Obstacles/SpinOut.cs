using UnityEngine;
using System.Collections;

public class SpinOut : MonoBehaviour 
{
    public int RotationalSpeed;
    [Range(0,1)] public float HorizontalFactor;
    public float Height;
    public int HeightSpeed;

    private float _startingHeight;
    private bool _goUp;

    void Start()
    {
        _startingHeight = transform.position.y;
        _goUp = true;
    }

	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(RotationalSpeed * Vector3.back * Time.deltaTime);

        // x position
        float x = transform.position.x + HorizontalFactor;

        // y position
        float y;
        if (transform.position.y < _startingHeight + Height - 1.7f && _goUp)
            y = Mathf.Lerp(transform.position.y, Height, Time.deltaTime * HeightSpeed);
        else
        {
            _goUp = false;
            y = Mathf.MoveTowards(transform.position.y, -Height, Time.deltaTime * HeightSpeed * 2);
        }

        if (transform.position.y < _startingHeight - Height)
            Destroy(gameObject);

        transform.position = new Vector3(x, y);
	}
}