using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    private bool isUsed;
    public Transform target;

    // Initialization
    private void Awake()
    {
        isUsed = false;
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (!isUsed && col.tag == "Player") {
            isUsed = true;
            col.transform.position = target.position;
        }
    }
}
