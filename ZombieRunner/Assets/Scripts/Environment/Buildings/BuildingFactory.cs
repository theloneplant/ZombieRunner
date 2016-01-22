using UnityEngine;
using UnityEditor;
using System.Collections;

public class BuildingFactory
{
    const float hitBoxHeight = 0.1f;
    
    // Arrays of assets eventually
    private Sprite wall;

    public BuildingFactory()
    {
        // Load all assets for building assembly
        wall = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Standard Assets/2D/Sprites/BackgroundNavyGridSprite.png");
    }

    public GameObject makeBuilding(float width, float height)
    {
        GameObject building = new GameObject();

        // Make main wall
        building.AddComponent<SpriteRenderer>();
        building.GetComponent<SpriteRenderer>().sprite = wall;
        Vector2 wallDims = wall.bounds.max - wall.bounds.min;
        building.transform.localScale = new Vector3(width / wallDims.x, height / wallDims.y, 1);
        // Maybe add tiling scale?

        // Make platform
        building.AddComponent<BoxCollider2D>();
        building.GetComponent<BoxCollider2D>().offset = new Vector2(wallDims.x / 2, wallDims.y - hitBoxHeight / 2);
        building.GetComponent<BoxCollider2D>().size = new Vector2(wallDims.x, hitBoxHeight);

        // One way platform
        building.AddComponent<OneWayPlatform>();

        // Scroll
        building.AddComponent<ScrollLeft>();

        // Set tags and layer
        building.tag = "Building";
        building.layer = LayerMask.NameToLayer("Buildings");

        return building;
    }
}
