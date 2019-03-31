using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Note-> Currently weapon script adds damage to this, making damage = bulletDamage + weaponDamage
    public float damage;
    public float speed;
    public float defensePenetration;

    protected Rigidbody2D rb;

    // Initialization
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 1.5f);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Enemy") {
            // Do damage to the enemy
            float[] array = { damage, defensePenetration };
            collision.transform.SendMessage("Damage", array);
        }
        Destroy(gameObject);
    }

    public void multiplyDamage(float ratio)
    {
        damage *= ratio;
    }

    public void setDamage(float d) {
        damage = d;
    }

    public void setSpeed(float s) {
        speed = s;
    }
}
