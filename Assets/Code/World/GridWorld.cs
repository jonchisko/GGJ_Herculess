using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class GridWorld : MonoBehaviour
{

    [SerializeField] 
    private int xDim = 40;

    [SerializeField] 
    private int yDim = 25;
    
    [SerializeField]
    private GameObject gridWorldParent;

    [SerializeField] 
    private int selectedBoxIndx;
    
    [SerializeField] 
    private int selectedBoxIndy;
    
    private TileBehaviour[] children;
    
    
    private void Awake()
    {
        children = gridWorldParent.transform.GetComponentsInChildren<TileBehaviour>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        ResizeNeighbourhood(selectedBoxIndx, selectedBoxIndy);
        PrintOutChildren();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void PrintOutChildren()
    {
        foreach (var child in children)
        {
            Debug.Log(child);
            Debug.Log(child.GetSurfacePosition());
        }
    }
    
    private void ResizeNeighbourhood(int indx, int indy)
    { 
        // todo: fix neighbourhood to work on edges and 8 neighbours and to accept the function that modifies the stuff
        ResizeBox(indx, indy-1);
        ResizeBox(indx, indy+1);
        ResizeBox(indx-1, indy);
        ResizeBox(indx+1, indy);
    }
    
    private void ResizeBox(int indx, int indy)
    {
        children[ConvertIndexToLinear(indx, indy)].transform.localScale = children[ConvertIndexToLinear(indx, indy)].transform.localScale * 1.1f;
    }

    private int ConvertIndexToLinear(int indx, int indy)
    {
        return xDim * indy + indx;
    }
    
}
