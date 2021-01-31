using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWineComponent : MonoBehaviour
{
    public GameObject winePrefab;
    public int worldX;
    public int worldY;

    public int worldZ;
    public void spawnObject()
    {
        Vector3 p = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint( p);
        

        GameObject a = Instantiate(winePrefab) as GameObject;
        a.transform.position =  new Vector3(worldX, worldY, worldZ); //pos +
    }
}
