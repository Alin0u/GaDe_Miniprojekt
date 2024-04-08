using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondCamera;
    private ArrowMovement arrowMovement;
    private TargetArrowBarMovement targetArrowBarMovement;

    void Start()
    {
        mainCamera.enabled = true;
        secondCamera.enabled = false;

        // Find the arrow-object
        arrowMovement = FindObjectOfType<ArrowMovement>();
        targetArrowBarMovement = FindObjectOfType<TargetArrowBarMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.enabled = !mainCamera.enabled;
            secondCamera.enabled = !secondCamera.enabled;

            // Calculate the arrow's position relative to the movement range
            float relativePosition = (targetArrowBarMovement.transform.position.x - targetArrowBarMovement.startingPosition.x) / targetArrowBarMovement.movementDistance;

            float speedMultiplier;
            if(relativePosition <= 0.3f)
            {
                speedMultiplier = 0.3f;
            }
            else if(relativePosition > 0.3f && relativePosition <= 0.7f)
            {
                speedMultiplier = 1f;
            }
            else
            {
                speedMultiplier = 0.3f;
            }

            // Set the functionality in percentage of how good the keys (up, down, right, left) work
            arrowMovement.SetSpeedMultiplier(speedMultiplier);

            // Start the movement fuction for the arrow
            arrowMovement.StartMovingForward();

            // Output for debugging & presentation
            Debug.Log("Speed: " + speedMultiplier);
        }
    }
}
