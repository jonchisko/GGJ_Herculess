using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;


public struct HerculessDirection
{
    public HerculessDirection(int x1, int y1, int x2, int y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }

    public int X1 { get; }
    public int Y1 { get; }
    public int X2 { get; }
    public int Y2 { get; }

    public bool IsDiagonal()
    {
        return !(X1 == X2 || Y1 == Y2);
    }
    
}

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

    [SerializeField] 
    private HerculessMover herculess;
    
    private TileBehaviour[] children;

    private List<TileBehaviour> tileMoves;
    
    private void Awake()
    {
        tileMoves = new List<TileBehaviour>();
        
        children = gridWorldParent.transform.GetComponentsInChildren<TileBehaviour>();
        
        tileMoves.Add(children[2]);
        tileMoves.Add(children[3]);
        tileMoves.Add(children[4]);
        tileMoves.Add(children[5]);
        tileMoves.Add(children[6]);
        tileMoves.Add(children[7]);
        tileMoves.Add(children[8]);
    }

    // Start is called before the first frame update
    private void Start()
    {
        ResizeNeighbourhood(selectedBoxIndx, selectedBoxIndy);
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnMoveCommandFinished(HerculessMover herculessMover)
    {
        if (tileMoves.Count > 0)
        {
            TileBehaviour tile = tileMoves[0];
            tileMoves.RemoveAt(0);
            
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

    #region Simulate Code

    private void SimulateHerculess(int startX, int startY)
    {
        TileBehaviour currentTile = children[ConvertIndexToLinear(startX, startY)];
        HerculessDirection currentDirection = new HerculessDirection(startX, startY, startX, startY + 1);
        HerculessDirection previousDirection = new HerculessDirection();
        
        while (!currentTile.isGoalTile)
        {
            //if (currentTile.placedUnitRef)
            
            //currentTile = 
        }
        // repeat loop until finish tile reached
        // check current tile and set direction by the tile effect
        // check if you can move into that direction, check if that tile is an obstacle - if it is, change direction by the type of obstacle
        
    }

    #endregion
}
