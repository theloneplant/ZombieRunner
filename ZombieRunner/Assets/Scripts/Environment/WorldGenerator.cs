using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour
{
    public GameObject player;
    public Camera camera;

    public GameObject[] potions;
    public float potionSpawnRate;

    public GameObject[] obstacles;
    public float obstacleSpawnRate;

    public GameObject[] buildings;
    public float buildingSpawnRate;

    private BuildingFactory buildingFactory;
    private float potionStartTime;
    private float buildingStartTime;

    void Start ()
    {
        buildingFactory = new BuildingFactory();

        potionStartTime = Time.time;
	}
	
	void Update ()
    {
	    if (Time.time - potionStartTime > potionSpawnRate)
        {
            makePotion();
            potionStartTime = Time.time;
        }

        if (Time.time - buildingStartTime > buildingSpawnRate)
        {
            makeBuilding();
            buildingStartTime = Time.time;
        }
    }

    private void makePotion()
    {
        int range = Random.Range(0, potions.Length);
        GameObject potion = Instantiate(potions[range]);

        // Set the potion to be off screen and make sure it removes itself later on
        moveRight(ref potion);
        addCleanup(ref potion);

        // Make the potion a child of the world generator
        potion.transform.parent = transform;

        float bottom = camera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f)).y;
        float y = camera.ViewportToWorldPoint(new Vector2(0.8f, 0.8f)).y - bottom;
        potion.transform.position = new Vector3(potion.transform.position.x, bottom + Random.Range(0f, y), potion.transform.position.z);

        // Initialize potion, used in place of Start() due to lack of control
        potion.GetComponent<Potion>().initStartHeight();
    }

    private void makeObstacle()
    {
        int range = Random.Range(0, obstacles.Length);
        GameObject obstacle = Instantiate(obstacles[range]);

        // Set the obstacle to be off screen and make sure it removes itself later on
        moveRight(ref obstacle);
        addCleanup(ref obstacle);

        // Make the obstacle a child of the world generator
        obstacle.transform.parent = transform;

        // TODO: Add obstacle specific properties
    }

    private void makeBuilding()
    {
        int range = Random.Range(0, buildings.Length);
        GameObject building = buildingFactory.makeBuilding(10, Random.Range(5f, 10f));

        // Set the building to be off screen and make sure it removes itself later on
        moveRight(ref building);
        addCleanup(ref building);

        // Make the building a child of the world generator
        building.transform.parent = transform;

        // Add player to check for one way platform
        building.GetComponent<OneWayPlatform>().player = player.GetComponent<BoxCollider2D>();

        // TODO: Add building specific properties
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
