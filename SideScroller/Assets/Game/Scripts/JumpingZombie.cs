using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingZombie : EnemyBehaviour
{
    protected BoxCollider2D mBoxCollider;
    public GameObject mGameObject;
    private bool mGrounded = true;
    
    // Start is called before the first frame update
    private new void Awake()
    {
        base.Awake();
        maxSpeed = 7f;
        maxDistance = 12f;
        attackDistance = 1.5f;
        attackRate = 4f;

        mGameObject = this.gameObject;

        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        mBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (curHealth <= 0)
        {
            m_dead = true;
            mBoxCollider.isTrigger = true;
            m_Rigidbody2D.velocity = Vector2.zero;
            FadeOut(0, 25f);
            Destroy(gameObject, 1f);
        }
        else
        {
            float range = Vector2.Distance(transform.position, Player.position);
            if (range > attackDistance && range < maxDistance)
            {
                MoveTowardsPlayer();
            }
            else
            {

                if (range <= attackDistance)
                {
                    if (Time.time > timeToFire)
                    {
                        timeToFire = Time.time + 1 / attackRate;
                        float[] array = { attackPower, 0 };
                        Player.SendMessage("Damage", array);
                    }

                }
            }
            flipCheck();
        }
    }

    private void MoveTowardsPlayer()
    {

        Vector3 moveGoal = Player.position;

        if (mGrounded)
        {
            //transform.position = Vector2.MoveTowards(transform.position, moveGoal, maxSpeed * Time.deltaTime);
            m_Rigidbody2D.AddForce(new Vector2(0, 90), ForceMode2D.Impulse);
            mGrounded = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, moveGoal, maxSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SolidPlatform")
            mGrounded = true;
    }
}
