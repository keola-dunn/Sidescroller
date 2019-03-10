using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : Bullet
{
    
    // The explosive force is weaker if the enemy is further from the center
    public float force = 100f;
    public float liftModifier = 0.2f;

    private CircleCollider2D explosionCircle;

    // Initialization
    protected override void Awake()
    {
        // isTrigger should be set to true in the prefab circle collider
        explosionCircle = GetComponent<CircleCollider2D>();
        damage = 25f;
        speed = 0f;
        defensePenetration = 1f;
        Destroy(gameObject, 0.1f);
    }

    private void Update() 
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, explosionCircle.radius*transform.localScale.x);
        for (int i = 0; i < collidersInRange.Length; ++i) {
            if (collidersInRange[i].gameObject.tag == "Enemy") {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, 
                                  (collidersInRange[i].transform.position-transform.position).normalized, 
                                  explosionCircle.radius*transform.localScale.x);
                if (hit.collider.gameObject == collidersInRange[i].gameObject) {
                    applyDamageAndForce(collidersInRange[i]);
                }
            }
        }
    }

    private void applyDamageAndForce(Collider2D collision)
    {
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

    // Explosion should be a trigger, OnTriggerEnter2D takes a Collider2D whereas OnCollisionEnter2D takes a Collision2D
    /*
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
    */
}
