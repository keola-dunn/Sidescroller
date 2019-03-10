using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltShot : MonoBehaviour
{
    public float damage = 20f;
    public float speed;
    public float defensePenetration = 2f;

    protected Rigidbody2D rb;

    // Initialization
    protected virtual void Awake()
    {


        Transform playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Vector3 difference = playerTransform.position - transform.position;


        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        if(difference.x > 0)
        {
            rb.velocity = transform.right * speed;
        }
        else
        { 
            rb.velocity = transform.right *- speed;
        }
        
        
        //Destroy(gameObject, 3f);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Do damage to the enemy
            float[] array = { damage, defensePenetration };
            collision.transform.SendMessage("Damage", array);
            Destroy(gameObject);
        }
    }

    public void multiplyDamage(float ratio)
    {
        damage *= ratio;
    }

}