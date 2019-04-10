using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglingPlatform : MonoBehaviour
{

    public float vanishTime;
    public float solidTime;
    private float timeToChange;
    private bool isSolid;
    private BoxCollider2D collider;

    // Initialization
    private void Awake() 
    {
        timeToChange = Time.time + solidTime;
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > timeToChange) {
            if (isSolid) {
                timeToChange = Time.time + vanishTime;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                collider.isTrigger = true;
                isSolid = false;
            } else {
                timeToChange = Time.time + solidTime;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                collider.isTrigger = false;
                isSolid = true;
            }
        }
    }
}
