using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 origin;
    public Transform destinationObj;
    private Vector2 dest;
    public float speed;
    public float waitTime;
    private float timeToMove;
    public float startDelay;

    // Initialization
    private void Awake()
    {
        origin = transform.position;
        dest = destinationObj.position;
        timeToMove = Time.time + startDelay;
    }

    // Update is called once per frame
    private void Update()
    {
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
}
