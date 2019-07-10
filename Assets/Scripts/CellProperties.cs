// ================================================================================================================================
// File:        CellProperties.cs
// Description:	Tracks whether a specific cell is dead or alive, used to toggle between these two states
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellProperties : MonoBehaviour
{
    private Renderer CellRenderer;  //Used to changed the cells material
    public Material LivingMaterial;  //Material applied to living cells
    public Material DeadMaterial;   //Material applied to dead cells

    public bool IsAlive = false;    //Tracks if this cell is alive or dead
    public bool NewState = false;   //New status of this cell to be applied at the end of the update loop (true = set to living, false = set to dead)

    public int CellRow = -1;    //Which row of the grid this cell belongs in
    public int CellColumn = -1; //Which column of the grid this cell belongs in

    void Awake()
    {
        //Store reference to the cells renderer component so the material can be easily applied when changing states
        CellRenderer = GetComponent<Renderer>();
    }

    //Kills living cells, revives dead cells
    public void ToggleState()
    {
        //Toggle the cells living status
        IsAlive = !IsAlive;
        
        //Update its material accordingly
        CellRenderer.material = IsAlive ? LivingMaterial : DeadMaterial;
    }

    //Remembers what state this cell needs to be set to at the end of the current update loop
    public void SetNewState(bool IsLiving)
    {
        //Store the new state to be applied
        NewState = IsLiving;
    }

    //Sets the Cell to the new State that it has previously been told to change to during the Update loop
    public void UpdateState()
    {
        //Kill the Cell if its being told to be killed
        if(IsAlive && !NewState)
            ToggleState();
        //Bring the Cell to life if its being told to come alive
        else if(!IsAlive && NewState)
            ToggleState();
    }
}
