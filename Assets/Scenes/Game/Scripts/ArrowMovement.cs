using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class ArrowMovement : MonoBehaviour
{
    public GameObject arrow;
    public AnimationClip animationClip;
    public float speed = 5.0f;
    public float strafeSpeed = 5.0f;
    public float boostMultiplier = 10.0f;
    private float speedMultiplier = 1f;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI instructionText;

    private bool isMovingForward = false;
    private bool isBoosting = false;
    private bool canMove = false;
    private bool gameOver = false;

    private Vector3 currentRotation = Vector3.zero;

    private void Start()
    {
        if (hitText != null) hitText.gameObject.SetActive(false);
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (instructionText != null) instructionText.gameObject.SetActive(false);
        arrow.GetComponent<Animator>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (!canMove)
            return;
        
        arrow.GetComponent<Animator>().enabled = true;
        float moveHorizontal = Input.GetAxis("Horizontal") * strafeSpeed * speedMultiplier * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * strafeSpeed * speedMultiplier * Time.deltaTime;

        // Move the arrow left and right, up and down
        transform.Translate(moveHorizontal, moveVertical, 0);

        // Calculate new rotation based on horizontal and vertical input
        float pitch = moveVertical * 90; 
        float yaw = moveHorizontal * 90;
    
        pitch = Mathf.Clamp(pitch, -22.5f, 22.5f);
        yaw = Mathf.Clamp(yaw, -22.5f, 22.5f);
        Vector3 targetRotation = new Vector3(-pitch, yaw, 0);
    
        // Smoothly interpolate to the new rotation
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime * 5);
        transform.localEulerAngles = currentRotation;

        // Arrow starts moving when second camera will be entered
        if (isBoosting)
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
        canMove = true;                                                                                                                                                                                                           
    }

    public void OnBoost(InputValue value)
    {
        isBoosting = value.isPressed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            if (hitText != null)
            {
                arrow.GetComponent<Animator>().enabled = false;
                canMove = false;
                speed = 0f;
                isMovingForward = false;
                isBoosting = false;
                hitText.gameObject.SetActive(true);
            }
        }

        else if (other.gameObject.tag == "environment")
        {
            gameOverText.gameObject.SetActive(true);
            instructionText.gameObject.SetActive(true);
            arrow.GetComponent<Animator>().enabled = false;
            canMove = false;
            speed = 0f;
            isMovingForward = false;
            isBoosting = false;
            gameOver = true;
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}