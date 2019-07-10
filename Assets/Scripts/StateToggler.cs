// ================================================================================================================================
// File:        StateToggler.cs
// Description:	Allows the user to toggle the state of any cell by right clicking on it
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateToggler : MonoBehaviour
{
    void Update()
    {
        //Detect right click input from the user
        if(Input.GetMouseButtonDown(1))
        {
            //Cast a ray from the mouse cursor through the scene camera to see which cell was clicked on (if any)
            RaycastHit CellHit;
            Ray CastingRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(CastingRay, out CellHit, 100.0f))
            {
                //Toggle the living status of the cell that was clicked on
                CellHit.transform.GetComponent<CellProperties>().ToggleState();
            }
        }
    }
}
