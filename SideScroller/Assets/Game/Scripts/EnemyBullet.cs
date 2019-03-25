using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    // Initialization
    private void Awake()
    {
        damage = 10f;
        speed = 12.5f;
        base.Awake();
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
