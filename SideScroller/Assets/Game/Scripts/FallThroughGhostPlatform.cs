using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThroughGhostPlatform : MonoBehaviour
{

    private CircleCollider2D playerLegs;

    // Initialization
    private void Awake() 
    {
        playerLegs = GetComponent<CircleCollider2D>();
    }

    // private void Update() 
    // {
    //     if (Input.GetKey(KeyCode.S)) {
    //         playerLegs.isTrigger = true;
    //     } else {
    //         playerLegs.isTrigger = false;
    //     }
    // }

    private void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "GhostPlatform" && Input.GetKey(KeyCode.S)) {
            playerLegs.isTrigger = true;
            // collision.gameObject.layer = LayerMask.NameToLayer("GhostPlatformFellThrough");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerLegs.isTrigger = false;
        // if (collision.gameObject.tag == "GhostPlatform") {
        //     collision.gameObject.layer = LayerMask.NameToLayer("GhostPlatform");
        // }
    }
    
}
