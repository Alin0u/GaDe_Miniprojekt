using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float strafeSpeed = 5.0f;
    public TextMeshProUGUI hitText;

    private bool isMovingForward = false;
    private bool canMove = true;

    private void Start()
    {
        if(hitText != null) hitText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!canMove)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal") * strafeSpeed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * strafeSpeed * Time.deltaTime;

        // Move the arrow left and right, up and down
        transform.Translate(moveHorizontal, moveVertical, 0);

        // Arrow starts moving when second camera will be entered
        if (isMovingForward)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    public void StartMovingForward()
    {
        isMovingForward = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Target")
        {
            if(hitText != null)
            {
                speed = 0f;
                hitText.gameObject.SetActive(true);
                canMove = false;
            }
        }
    }
}
