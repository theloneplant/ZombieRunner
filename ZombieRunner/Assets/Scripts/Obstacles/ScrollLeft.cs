using UnityEngine;
using System.Collections;

public class ScrollLeft : MonoBehaviour 
{
    public void Update()
    {
        transform.position = new Vector3(transform.position.x - (Z_Globals.RunSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }
}