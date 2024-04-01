using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrowBarMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float movementDistance = 10.0f;
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, movementDistance);
        transform.position = startingPosition + new Vector3(movement, 0, 0);
    }
}
