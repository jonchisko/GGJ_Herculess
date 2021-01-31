using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thunderPrefab;
    public int worldX;
    public int worldY;

    public int worldZ;
    public void spawnObject()
    {
        Vector3 p = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint( p);
        

        GameObject a = Instantiate(thunderPrefab) as GameObject;
        a.transform.position =  new Vector3(worldX, worldY, worldZ); //pos +
    }
}
