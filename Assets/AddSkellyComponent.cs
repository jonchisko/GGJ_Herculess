using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSkellyComponent : MonoBehaviour
{
    public GameObject skellyPrefab;
    public int worldX;
    public int worldY;

    public int worldZ;
    public void spawnObject()
    {
        Vector3 p = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint( p);
        

        GameObject a = Instantiate(skellyPrefab) as GameObject;
        a.transform.position =  new Vector3(worldX, worldY, worldZ); //pos +
    }
}
