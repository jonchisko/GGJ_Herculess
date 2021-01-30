using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public TileTypes type;
    public bool hasUnit;
    public bool isRange;
    public GameObject placedUnitRef;

    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetSurfacePosition()
    {
        Vector3 tmp = this.transform.position;
        return new Vector3(tmp.x,  tmp.y + renderer.bounds.size.y/2.0f, tmp.z);
    }

    private void OnDrawGizmos()
    {
        if (renderer != null)
        {
            var a = GetSurfacePosition();
            Gizmos.DrawCube(a, Vector3.one/10.0f);
        }
    }
}
