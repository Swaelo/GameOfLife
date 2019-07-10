// ================================================================================================================================
// File:        CameraController.cs
// Description:	Used to allow adjustment of camera zoom and position during gameplay
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float ZoomSpeed = 1.5f;  //How fast the camera's zoom is adjusted while scrolling the mouse wheel
    private float CurrentZoom = 5f;    //Current zoom level of the scene camera
    private float MinimumZoom = 5f;    //The closest the camera can zoom into the game
    private float MaximumZoom = 500f;   //The furthest the camera can zoom out from the game

    public float CurrentX = 2f;   //Current X location of the camera
    public float CurrentZ = 3.5f;  //Current Z location of the camera
    private float ScrollSpeed = 0.25f;  //How fast the X/Z position is updated from input
    private float MoveSpeed = 1.25f;   //How fast the cameras position is moved

    void Update()
    {
        //Adjust location of the camera
        AdjustPosition();
        //Adjust zoom level with the mouse wheel
        AdjustZoom();
    }

    void LateUpdate()
    {
        //Adjust the size of the cameras orthographic projection to give the illusion of zoom
        GetComponent<Camera>().orthographicSize = CurrentZoom;

        //Create a new Vector3 location for the cameras target position and move it there
        Vector3 CameraPosition = new Vector3(CurrentX, 15, CurrentZ);
        transform.position = CameraPosition;
    }

    //Allow the user to adjust the cameras zoom level with the mouse scroll wheel
    private void AdjustZoom()
    {
        //Apply mousewheel input onto the current zoom level
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        //Clamp the zoom level between the allowed min/max values
        CurrentZoom = Mathf.Clamp(CurrentZoom, MinimumZoom, MaximumZoom);
    }

    //Allow the user to move the cameras position with the WASD/Arrow Keys
    private void AdjustPosition()
    {
        //Move the camera along the X axis with A/D or Left/Right Arrows
        CurrentX += Input.GetAxis("Horizontal") * ScrollSpeed;
        //Move the camera along the Z axis with W/S or Up/Down Arrows
        CurrentZ += Input.GetAxis("Vertical") * ScrollSpeed;
    }
}
