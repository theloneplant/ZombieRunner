using UnityEngine;
using System.Collections;

public class ScrollParallax : MonoBehaviour
{
    public Transform[] backgrounds;             // Array of all the backgrounds to be parallaxed.
    public float parallaxScale;                 // The proportion of the camera's movement to move the backgrounds by.
    public float parallaxReductionFactor;       // How much less each successive layer should parallax.
    public float yReductionFactor;              // How much less the Y axis should be affected
    public float smoothing;                     // How smooth the parallax effect should be.
    
    private Transform cam;                      // Shorter reference to the main camera's transform.
    private Vector3 startCamPos;				// Original camera position
    private Vector2[] startTexOffsets;          // Original texture offsets
    private Vector3[] startOffsets;             // Original position offsets

    void Awake()
    {
        // Setting up the reference shortcut.
        cam = Camera.main.transform;
    }

    void Start ()
    {
        startCamPos = cam.position;
        startOffsets = new Vector3[backgrounds.Length];

        startTexOffsets = new Vector2[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Save offset to reset later; makes sure we don't save the offset permanently
            startTexOffsets[i] = backgrounds[i].gameObject.GetComponent<Renderer>().materials[0].GetTextureOffset("_MainTex");
            startOffsets[i] = new Vector3(backgrounds[i].position.x, backgrounds[i].position.y, backgrounds[i].position.z);
        }

        Z_Globals.RunSpeed = 0.5f; // TODO: Change this to be in player
	}
	
	void Update ()
    {
        // The parallax is opposite of the camera movement
        float parallax = (cam.position.y - startCamPos.y) * parallaxScale;
        
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Offset height by parallax
            float backgroundTargetPosY = startOffsets[i].y + parallax * (i * parallaxReductionFactor * yReductionFactor);

            Vector3 backgroundTargetPos = new Vector3(backgrounds[i].position.x, backgroundTargetPosY, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

            // Calculate horizontal scroll
            float x = Mathf.Repeat(Time.time * Z_Globals.RunSpeed * (1 - (i + 1) * parallaxReductionFactor), 1);
            Vector2 offset = new Vector2(x, startTexOffsets[i].y);
            backgrounds[i].gameObject.GetComponent<Renderer>().materials[0].SetTextureOffset("_MainTex", offset);
        }
    }

    void OnDisable ()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Save offset to reset editor values
            startTexOffsets[i] = backgrounds[i].gameObject.GetComponent<Renderer>().materials[0].GetTextureOffset("_MainTex");
            backgrounds[i].gameObject.GetComponent<Renderer>().materials[0].SetTextureOffset("_MainTex", startTexOffsets[i]);
        }
    }
}
