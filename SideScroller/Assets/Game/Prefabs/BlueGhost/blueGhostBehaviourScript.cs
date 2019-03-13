using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueGhostBehaviourScript : EnemyBehaviour
{

    private bool outBool;

    private EnemyHealthBar mHealthBar;

    private new void Awake()
    {
        base.Awake();
        maxSpeed = 8f;
        maxDistance = 6f;
        attackDistance = 1.70f;

        maxHealth = 100f;
        curHealth = 100f;
        defense = 1;
        attackPower = 20f;
        attackRate = 4f;
        m_FacingRight = true;

        FreeYMovement = true;

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

            if (range > attackDistance && range < maxDistance)
            {
                m_Anim.SetBool("Out", true);
                outBool = true;

                moveTowardsPlayer();

            }
            else
            {
                if (range <= attackDistance)
                {
                    // Check time is after the time a shot is available
                    if (Time.time > timeToFire)
                    {
                        timeToFire = Time.time + 1 / attackRate;

                        float[] array = { attackPower, 0 };
                        Player.SendMessage("Damage", array);
                    }

                }
                else
                {
                    outBool = false;
                    m_Anim.SetBool("Out", false);
                }
            }

            //flipping wolf character to follow
            if (Player.position.x > transform.position.x && m_FacingRight)
            {
                Flip();
            }
            else if (Player.position.x < transform.position.x && !m_FacingRight)
            {
                Flip();
            }


        }
    }


    public new void Damage(float[] attr)
    {
        //If defense is greater than or equal to damage taken, 1 damage is taken instead

        if (outBool)
        {
            if (attr[0] <= (defense - attr[1]))
            {
                curHealth--;
            }
            else
            {
                curHealth -= (attr[0] - Mathf.Max(0, (defense - attr[1])));
            }
            mHealthBar.ChangeHealth(curHealth, maxHealth);
        }

    }

}
