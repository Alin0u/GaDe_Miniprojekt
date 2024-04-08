using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ArrowMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float strafeSpeed = 5.0f;
    public float boostMultiplier = 10.0f;
    public TextMeshProUGUI hitText;

    private bool isMovingForward = false;
    private bool isBoosting = false;

    private void Start()
    {
        if(hitText != null) hitText.gameObject.SetActive(false);
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * strafeSpeed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * strafeSpeed * Time.deltaTime;

        // Move the arrow left and right, up and down
        transform.Translate(moveHorizontal, moveVertical, 0);

        // Arrow starts moving when second camera will be entered
        if(isBoosting)
        {
            // Increase the forward movement speed
            transform.Translate(0, 0, speed * boostMultiplier * Time.deltaTime);
        }
        else if (isMovingForward)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    public void StartMovingForward()
    {
        isMovingForward = true;
    }

    public void OnBoost(InputValue value)
    {
        isBoosting = value.isPressed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Target")
        {
            if(hitText != null)
            {
                speed = 0f;
                isMovingForward = false;
                isBoosting = false;
                hitText.gameObject.SetActive(true);
            }
        }
    }
}
