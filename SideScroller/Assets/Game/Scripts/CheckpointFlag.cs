using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFlag : MonoBehaviour
{

    public Sprite afterSetSprite;

    // Initialization
    private void Awake() {}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            GetComponent<SpriteRenderer>().sprite = afterSetSprite;
        }
    }
}
