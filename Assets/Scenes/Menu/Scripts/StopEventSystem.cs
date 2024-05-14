using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopEventSystem : MonoBehaviour
{
    void OnEnable() {
        EventSystem[] systems = FindObjectsOfType<EventSystem>();
        if (systems.Length > 1) {
            Destroy(this.gameObject.GetComponent<EventSystem>()); // Destroy this scene's event system if another one exists
        }
    }
}
