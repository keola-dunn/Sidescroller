using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardRocket : Bullet
{

    public GameObject explosionGameObject;

    // Initialization
    protected override void Awake()
    {
        damage = 50f;
        speed = 15f;
        defensePenetration = 2f;
        base.Awake();
    }

    // Update is called once per frame
    protected override void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Enemy") {
            // Do damage to the enemy
            float[] array = { damage, defensePenetration };
            collision.transform.SendMessage("Damage", array);
        }
        Destroy(gameObject);
        GameObject generatedExplosion = Instantiate(explosionGameObject, transform.position, Quaternion.identity);
    }
}
