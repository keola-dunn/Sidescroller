using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 origin;
    private Transform destination;
    private Vector2 dest;
    public float speed;
    public float waitTime;
    private float timeToMove;
    public float startDelay;
    public bool touchToMove;

    // Initialization
    private void Awake()
    {
        destination = transform.Find("Destination");
        origin = transform.position;
        dest = destination.position;
        timeToMove = Time.time + startDelay;
    }

    // Update is called once per frame
    private void Update()
    {
        if (touchToMove) {
            return;
        }
        if (Time.time > timeToMove) {
            if (Vector2.Distance(transform.position, dest) <= 0.01) {
                Vector2 temp = origin;
                origin = dest;
                dest = temp;
                timeToMove = Time.time + waitTime;
            }
            transform.position = Vector2.MoveTowards(transform.position, dest, speed * Time.deltaTime);
        }
    }

    public void characterTouch() {
        touchToMove = false;
    }
}
