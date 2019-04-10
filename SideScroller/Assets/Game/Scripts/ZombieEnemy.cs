using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : EnemyBehaviour
{

    protected BoxCollider2D mBoxCollider;
    public GameObject mGameObject;


    // Start is called before the first frame update
    private new void Awake()
    {
        base.Awake();
        maxSpeed = 6f;
        maxDistance = 10f;
        attackDistance = 1.5f;
        attackRate = 4f;

        mGameObject = this.gameObject;

        /**maxHealth = 25f;
        curHealth = 25f;
        defense = 1;
        attackPower = 10f;**/
        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        mBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
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
                moveTowardsPlayer();
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
}
