using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    // Initialization

    public int damageScene = 0;
    private void Awake()
    {
        speed = 15f;
        base.Awake();
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
