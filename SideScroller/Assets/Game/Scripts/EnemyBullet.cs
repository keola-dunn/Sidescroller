using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{

    public int damageScene = 0;
    protected override void Awake()
    {
        speed = 12f;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 1.75f);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player") {
            //Allows rewriting of basic enemyBullet damage in a prefab (if desired)
            if(damageScene > 0)
            {
                damage = damageScene;
            }
            float[] array = { damage, 0 };
            collision.transform.SendMessage("Damage", array);
        }
        Destroy(gameObject);
    }
}
