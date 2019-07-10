// ================================================================================================================================
// File:        CellArray.cs
// Description:	Used to spawn in and keep track of the grid of cells for the game
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellArray : MonoBehaviour
{
    //This is a singleton object so its easily accessed from all the CellProperties scripts in each Cell object
    public static CellArray Instance = null;
    void Awake() { Instance = this; }

    public GameObject CellPrefab;   //Prefab used when spawning new cells into the game world
    public GameObject[,] CellGrid;  //2D array used to store and keep track of every cell in the game
    private int GridSize;   //The dimensions of the current grid of cells existing in the game

    //Returns all of the cell objects in a List format
    public List<GameObject> GetCells()
    {
        //Create a new list to store all the cells
        List<GameObject> Cells = new List<GameObject>();

        //Loop through our 2D array and add each Cell to this list
        for(int x = 0; x < GridSize; x++)
        {
            for(int z = 0; z < GridSize; z++)
            {
                Cells.Add(CellGrid[x,z]);
            }
        }

        //Return the final list containing all the grid cells
        return Cells;
    }

    //Used to spawn in a new grid of cells when the game is being started
    public void SetupGrid(int GridSize)
    {
        //Store the size of the grid that we are creating
        this.GridSize = GridSize;

        //Start by initializing our 2D array where reference to each cell will be stored
        CellGrid = new GameObject[GridSize, GridSize];

        //Create a new Vector3 structure which will be passed into each Instantiate call when placing the grid cells
        Vector3 CellPosition = new Vector3(0,0,0);

        //Loop along the X axis to create each row of cells
        for(int x = 0; x < GridSize; x++)
        {
            //For each row, loop along and place each cell for the matching columns
            for(int z = 0; z < GridSize; z++)
            {
                //Spawn a new CellPrefab at the next CellPosition
                GameObject NewCell = GameObject.Instantiate(CellPrefab, CellPosition, Quaternion.identity);

                //Store the new cells grid coordinates inside the cells properties component
                CellProperties NewCellProperties = NewCell.GetComponent<CellProperties>();
                NewCellProperties.CellRow = x + 1;
                NewCellProperties.CellColumn = z + 1;

                //Store the new cell in our 2D array
                CellGrid[x,z] = NewCell;

                //Increment the CellPosition X position value for spawning in the next cell
                CellPosition.x += 1.1f;
            }

            //After each row is complete, reset the X value and increment the ZPos value for spawning in the next row of cells
            CellPosition.x = 0;
            CellPosition.z += 1.1f;
        }
    }

    //Returns a list containing all of a given cells neighbours
    public List<GameObject> GetNeighbours(GameObject TargetCell)
    {
        //Create a new list to store the target cells neighbours
        List<GameObject> Neighbours = new List<GameObject>();

        //Fetch the target cells grid coordinates from its properties component
        CellProperties TargetProperties = TargetCell.GetComponent<CellProperties>();
        int CellRow = TargetProperties.CellRow;
        int CellColumn = TargetProperties.CellColumn;

        //Compare the target cells grid coordinates to the current grid size so we know which sides it has neighbours on
        bool NorthNeighbours = CellRow < GridSize;
        bool EastNeighbours = CellColumn < GridSize;
        bool SouthNeighbours = CellRow > 1;
        bool WestNeighbours = CellColumn > 1;

        //Add all the neighbouring cells to the list, using these flags so we know which ones we can add
        int RowIndex = CellRow-1;
        int ColumnIndex = CellColumn-1;
        if(NorthNeighbours)
            Neighbours.Add(CellGrid[RowIndex+1,ColumnIndex]);
        if(NorthNeighbours && EastNeighbours)
            Neighbours.Add(CellGrid[RowIndex+1,ColumnIndex+1]);
        if(EastNeighbours)
            Neighbours.Add(CellGrid[RowIndex,ColumnIndex+1]);
        if(SouthNeighbours && EastNeighbours)
            Neighbours.Add(CellGrid[RowIndex-1,ColumnIndex+1]);
        if(SouthNeighbours)
            Neighbours.Add(CellGrid[RowIndex-1,ColumnIndex]);
        if(SouthNeighbours && WestNeighbours)
            Neighbours.Add(CellGrid[RowIndex-1,ColumnIndex-1]);
        if(WestNeighbours)
            Neighbours.Add(CellGrid[RowIndex,ColumnIndex-1]);
        if(NorthNeighbours && WestNeighbours)
            Neighbours.Add(CellGrid[RowIndex+1,ColumnIndex-1]);

        //Return the final list containing all of the target cells neighbours
        return Neighbours;
    }

    //Tells you how many cells in a given list are alive
    public int LivingCount(List<GameObject> CellList)
    {
        //Start the counter at 0
        int LivingCells = 0;

        //Loop through all the cells in the list
        foreach(GameObject Cell in CellList)
        {
            //Check each cells properties component to check if its alive
            if(Cell.GetComponent<CellProperties>().IsAlive)
                LivingCells++;
        }

        //Return the final count of living cells
        return LivingCells;
    }
}
