// ================================================================================================================================
// File:        GridSpawner.cs
// Description:	Used to configure the desired grid size then spawn it in to start the game when ready
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpawner : MonoBehaviour
{
    public GameObject SetupMenu;
    public GameObject GameMenu;

    void Awake()
    {
        //Disable the GameMenu object by default
        GameMenu.SetActive(false);
    }

    public void SetupGrid()
    {
        //Get the grid size that was entered into the UI
        int GridSize = Int32.Parse(GameObject.Find("Grid Size Input").GetComponent<InputField>().text);

        //Spawn in the grid of the given size
        GetComponent<CellArray>().SetupGrid(GridSize);

        //Change from the setup menu to the game menu
        SetupMenu.SetActive(false);
        GameMenu.SetActive(true);
    }
}
