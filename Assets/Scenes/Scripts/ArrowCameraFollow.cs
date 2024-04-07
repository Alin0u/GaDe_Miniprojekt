using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCameraFollow : MonoBehaviour
{
    public Transform arrow;

    void LateUpdate () {
        transform.position = arrow.transform.position + new Vector3(2f, 1f, -4f);
    }
}
