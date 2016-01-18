using UnityEngine;
using System.Collections;

/**
 * Stephen Trinh
 */
[RequireComponent(typeof(ScrollLeft))]
public class Obstacle : MonoBehaviour 
{
    [SerializeField][Range(0,1)] private float _spModifier;     // How much this obstacle will affect the colliding object's speed

    public GameObject DestroyedEffect;                          // Instantiated when this obstacle is destroyed

    public void Update()
    {
        float x = transform.position.x - (Z_Globals.RunSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var affected = (IAffectedByObstacle)other.GetComponent(typeof(IAffectedByObstacle));
        if (affected != null)
        {
            affected.TouchedObstacle(_spModifier);
            DestroyObstacle();
        }
    }

    public void DestroyObstacle()
    {
        if (DestroyedEffect != null)
        {
            Instantiate(DestroyedEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}