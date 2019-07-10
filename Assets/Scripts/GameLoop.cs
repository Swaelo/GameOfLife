// ================================================================================================================================
// File:        GameLoop.cs
// Description:	This file contains the main implementation of the rules of Conway's Game of Life, this implementation is based
// on the rules of the game as described on this wikipedia page https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public bool GameActive = false;  //Tracks if the game loop is currently active or not

    private float UpdateInterval = 1.0f;    //How often the game is updated
    private float NextUpdate = 1.0f;    //How long until the next game update

    //Triggered by UI button to pause/resume the game loop
    public void ToggleGameLoop()
    {
        //Toggle the current status of the game loop
        GameActive = !GameActive;

        //Update the text of the toggle button
        GameObject.Find("Toggle Button").GetComponentInChildren<Text>().text = GameActive ? "Pause" : "Resume";
        GameObject.Find("Loop Status").GetComponent<Text>().text = GameActive ? "Game is Active" : "Game is Paused";
    }

    void Update()
    {
        if(GameActive)
            UpdateGame();
    }

    private void UpdateGame()
    {
        //Count down until the next update should take place
        NextUpdate -= Time.deltaTime;

        //Perform the next game update and reset the timer whenever it reaches zero
        if(NextUpdate <= 0.0f)
        {
            //Reset the timer
            NextUpdate = UpdateInterval;

            //Update all the cells in the grid
            UpdateCells();
        }
    }

    //Performs a single update to all the cells in the grid
    public void UpdateCells()
    {
        //Fetch the total list of Cell objects
        List<GameObject> Cells = CellArray.Instance.GetCells();

        //Loop through and find out what the new value will be for all cells
        foreach(GameObject Cell in Cells)
        {
            //Fetch the properties component of the Cell we are about to update
            CellProperties Properties = Cell.GetComponent<CellProperties>();

            //Fetching all of this cells neighbours
            List<GameObject> Neighbours = CellArray.Instance.GetNeighbours(Cell);

            //Check how many of the cells neighbours are alive
            int LivingNeighbours = CellArray.Instance.LivingCount(Neighbours);

            //Apply Rule 1 (any living cell with <2 living neighbours is killed, as if by underpopulation)
            if(Properties.IsAlive && LivingNeighbours < 2)
                Properties.SetNewState(false);
            //Apply Rule 2 (any living cell with two or three living neighbours remains alive)
            else if(Properties.IsAlive && LivingNeighbours == 2 || LivingNeighbours == 3)
                Properties.SetNewState(true);
            //Apply Rule 3 (any living cell with >3 living neighbours is killed, as if by overpopulation)
            else if(Properties.IsAlive && LivingNeighbours > 3)
                Properties.SetNewState(false);
            //Apply Rule 4 (any dead cell with exactly 3 living neighbours is brought to life, as if by reproduction)
            else if(!Properties.IsAlive && LivingNeighbours == 3)
                Properties.SetNewState(true);
            //Otherwise we just tell the cell to remain in whatever state its currently in
            else
                Properties.SetNewState(Properties.IsAlive);
        }

        //Now each Cell knows what its new state needs to be, tell them all to change to that state
        foreach(GameObject Cell in Cells)
        {
            //Call the UpdateState function on all the Cells properties components
            Cell.GetComponent<CellProperties>().UpdateState();
        }
    }
}
