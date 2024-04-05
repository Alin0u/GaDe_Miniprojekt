using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondCamera;
    private ArrowMovement arrowMovement;

    private ArrowCameraMovement arrowCameraMovement;

    private bool spacePressed = false;

    void Start()
    {
        mainCamera.enabled = true;
        secondCamera.enabled = false;

        // Find the arrow-object
        arrowMovement = FindObjectOfType<ArrowMovement>();
        arrowCameraMovement = FindObjectOfType<ArrowCameraMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !spacePressed) // Check the flag here
        {
            mainCamera.enabled = !mainCamera.enabled;
            secondCamera.enabled = !secondCamera.enabled;

            // Start the movement function for the arrow
            arrowMovement.StartMovingForward();
            arrowCameraMovement.StartMovingForward();

            spacePressed = true; // Set the flag to true after the space bar is pressed
        }
    }
}
