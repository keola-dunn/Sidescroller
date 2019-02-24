using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Bullet
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        damage = 0;
        speed = 10f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 1.5f);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) 
    {

    }
}
