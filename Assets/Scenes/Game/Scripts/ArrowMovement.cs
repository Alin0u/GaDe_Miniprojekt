using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System.IO;

public class ArrowMovement : MonoBehaviour
{
    public GameObject arrow;
    public AnimationClip animationClip;
    public float speed = 5.0f;
    public float strafeSpeed = 5.0f;
    public float boostMultiplier = 10.0f;
    private float speedMultiplier = 1f;
    private int level = 1;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI levelText;
    public AudioSource backgroundMusic; 
    private bool isMovingForward = false;
    private bool isBoosting = false;
    private bool canMove = false;
    private bool gameOver = false;
    private bool isMenuLoaded = false;
    private bool levelCompleted = false;
    private Vector3 currentRotation = Vector3.zero;
    private string filePath;

    private void Start()
    {
        if (hitText != null) hitText.gameObject.SetActive(false);
        if (nextLevelText != null) nextLevelText.gameObject.SetActive(false);
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (instructionText != null) instructionText.gameObject.SetActive(false);
        arrow.GetComponent<Animator>().enabled = false;
        filePath = Application.persistentDataPath + "/level.txt";
        LoadLevel();
        levelText.text = "LEVEL: " + level;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (levelCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                LoadLevel();
                levelText.text = "LEVEL: " + level;
            }
        }

        if (!canMove || Time.timeScale == 0)
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
                nextLevelText.gameObject.SetActive(true);
                levelCompleted = true;
                level += 1;
                File.WriteAllText(filePath, level.ToString());
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
            File.WriteAllText(filePath, "1");
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void TogglePauseMenu()
    {
        if (!isMenuLoaded)
        {
            SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive).completed += (AsyncOperation op) =>
            {
                isMenuLoaded = true;
                Time.timeScale = 0; // Pause the game
                backgroundMusic.Pause();
            };
        }
        else
        {
            SceneManager.UnloadSceneAsync("MenuScene").completed += (AsyncOperation op) =>
            {
                isMenuLoaded = false;
                Time.timeScale = 1; // Resume the game
                backgroundMusic.Play();
            };
        }
    }

    private void LoadLevel()
    {
        if (File.Exists(filePath))
        {
            string levelString = File.ReadAllText(filePath); 
            if (int.TryParse(levelString, out int loadedLevel))
            {
                level = loadedLevel;
            }
        }
    }
}