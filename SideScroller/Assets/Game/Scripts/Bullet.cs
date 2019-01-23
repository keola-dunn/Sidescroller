using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float xSpeed = 20f;
    public float ySpeed = 20f;
    public float xDirection = 0f;
    public float yDirection = 0f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xVelocity = xSpeed * xDirection;
        float yVelocity = ySpeed * yDirection;
        rb.velocity = new Vector2(xVelocity, yVelocity);
        Destroy(gameObject, 1.5f);
    }
}
