using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondCamera;
    private ArrowMovement arrowMovement;

    void Start()
    {
        mainCamera.enabled = true;
        secondCamera.enabled = false;

        // Find the arrow-object
        arrowMovement = FindObjectOfType<ArrowMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.enabled = !mainCamera.enabled;
            secondCamera.enabled = !secondCamera.enabled;

            // Start the movement fuction for the arrow
            arrowMovement.StartMovingForward();
        }
    }
}
