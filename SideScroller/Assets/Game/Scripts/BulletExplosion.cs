using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : Bullet
{
    
    // The explosive force is weaker if the enemy is further from the center
    public float force = 100f;
    public float liftModifier = 0.2f;

    // Initialization
    protected override void Awake()
    {
        // isTrigger should be set to true in the prefab circle collider
        damage = 25f;
        speed = 0f;
        defensePenetration = 1f;
        Destroy(gameObject, 0.1f);
    }

    // Explosion should be a trigger, OnTriggerEnter2D takes a Collider2D whereas OnCollisionEnter2D takes a Collision2D
    protected void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Enemy") {
            // Do damage to the enemy
            float[] array = { damage, defensePenetration };
            collision.transform.SendMessage("Damage", array);

            Rigidbody2D enemyBody = collision.attachedRigidbody;
            Vector3 difference = (enemyBody.transform.position - transform.position);
            Vector3 baseForce = difference.normalized * force / difference.magnitude;
            enemyBody.AddForce(baseForce, ForceMode2D.Impulse);

            // Adds a force that pushes the enemy up
            Vector3 upliftForce = liftModifier * Vector2.up * force / difference.magnitude;
            enemyBody.AddForce(upliftForce, ForceMode2D.Impulse);
        }
    }

}
