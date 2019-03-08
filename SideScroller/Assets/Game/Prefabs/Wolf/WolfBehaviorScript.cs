using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class WolfBehaviorScript : EnemyBehaviour
{
    

    private new void Awake()
    {
        base.Awake();
        maxSpeed = 4f;
        maxDistance = 9f;
        attackDistance = 1.65f;

        maxHealth = 100f;
        curHealth = 100f;
        defense = 5;
        attackPower = 10f;
        attackRate = 2f;
        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (curHealth <= 0)
        {

            
                m_dead = true;
                m_Anim.SetBool("Dead", true);
                FadeOut(0f, 80f);
            Destroy(gameObject, 3f);
         
        }
        else
        {
             float range = Vector2.Distance(transform.position, Player.position);

            if (range > attackDistance && range < maxDistance) {
                m_Anim.SetBool("Run", true);

                moveTowardsPlayer();
                
            }
            else
            {
                m_Anim.SetBool("Run", false);
                if(range <= attackDistance)
                {
                   m_Anim.SetBool("Attacking", true);
                       // Check time is after the time a shot is available
                    if (Time.time > timeToFire)
                    {
                       timeToFire = Time.time + 1 / attackRate;
                   
                       float[] array = {attackPower, 0 };
                       Player.SendMessage("Damage", array);
                    }
                   
                }
                else
                {
                    m_Anim.SetBool("Attacking", false);
                }
            }
           


            flipCheck();

        }
    }

    

   
}




