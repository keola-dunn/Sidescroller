using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantsManBehaviour : EnemyBehaviour
{
    // Start is called before the first frame update


    public Bolt bolt;

    private EnemyHealthBar mHealthBar;

    protected bool isReloading;
    protected int reloadingCounter;
    protected float kickDistance = 1.2f;

    new void Awake()
    {
        base.Awake();
        m_dead = false;
        maxSpeed = 3f;
        maxDistance = 10f;
        attackDistance = 8f;

        maxHealth = 100f;
        curHealth = 100f;
        defense = 5;
        attackPower = 10f;
        attackRate = 2f;
        m_FacingRight = false;
        //mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
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
                m_Anim.SetBool("Running", true);

                moveTowardsPlayer();

            }
            else
            {
                m_Anim.SetBool("Running", false);
                if (range <= attackDistance)
                {
                    m_Anim.SetBool("Attacking", true);
                    if (!isReloading)
                    {
                        bolt.Shoot();
                        isReloading = true;
                    }
                    else
                    {
                        m_Anim.SetBool("Kicking", true);
                        reloadingCounter++;
                        if (reloadingCounter > 120)
                        {
                            reloadingCounter = 0;
                            isReloading = false;
                            m_Anim.SetBool("Kicking", false);
                        }

                        if(range >= kickDistance)
                        {

                            m_Anim.SetBool("Attacking", false);
                            m_Anim.SetBool("Kicking", false);
                            m_Anim.SetBool("Running", true);
                            moveTowardsPlayer();
                        }
                        else
                        {
                            Kick();
                        }
                    }

                }
                else
                {
                    m_Anim.SetBool("Attacking", false);
                    m_Anim.SetBool("Kicking", false);
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

    public void Kick()
    {

        m_Anim.SetBool("Kicking", true);
        if (Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / attackRate;
            float[] array = { attackPower, 3 };
            Player.SendMessage("Damage", array);
        }
    }



}