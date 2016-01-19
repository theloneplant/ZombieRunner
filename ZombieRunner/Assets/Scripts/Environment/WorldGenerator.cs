using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour
{
    public GameObject agilityPotion, strengthPotion;
    public float potionSpawnRate;

    private float potionStartTime;
    
	void Start ()
    {
        potionStartTime = Time.time;
	}
	
	void Update ()
    {
	    if (Time.time - potionStartTime > potionSpawnRate)
        {
            makePotion();
            potionStartTime = Time.time;
        }
	}

    private void makePotion()
    {
        int range = Random.Range(0, 100);

        GameObject potion;
        if (range < 50)
        {
            // Make agility potion
            potion = Instantiate(agilityPotion);
        }
        else
        {
            // Make strength potion
            potion = Instantiate(strengthPotion);
        }

        // Set the potion to be off screen and make sure it removes itself later on
        moveRight(ref potion);
        addCleanup(ref potion);

        // Make the potion a child of the world generator
        potion.transform.parent = transform;
    }

    private void makeObstacle()
    {

    }

    private void makeBuilding()
    {

    }

    private void moveRight(ref GameObject obj)
    {
        // Find the right edge of the screen and set the object's x to be just past it
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + 0.1f;
        obj.transform.position = new Vector3(rightBorder, obj.transform.position.y, obj.transform.position.z);
    }

    private void addCleanup(ref GameObject obj)
    {
        // Attach cleanup script
        if (obj.GetComponent<Z_Cleanup>() == null)
            obj.AddComponent<Z_Cleanup>();
    }
}
