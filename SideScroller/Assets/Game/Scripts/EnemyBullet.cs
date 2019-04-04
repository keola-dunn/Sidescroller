using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    // Initialization
    protected override void Awake()
    {
        speed = 15f;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 1.75f);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player") {
            // Do damage to the enemy
            float[] array = { damage, 0 };
            collision.transform.SendMessage("Damage", array);
        }
        Destroy(gameObject);
    }
}
