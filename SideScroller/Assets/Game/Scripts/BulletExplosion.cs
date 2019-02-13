using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : Bullet
{

    // Initialization
    protected override void Awake()
    {
        damage = 25f;
        speed = 0f;
        defensePenetration = 1f;
        Destroy(gameObject, 0.25f);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Enemy") {
            // Do damage to the enemy
            float[] array = { damage, defensePenetration };
            collision.transform.SendMessage("Damage", array);
        }
    }

}
