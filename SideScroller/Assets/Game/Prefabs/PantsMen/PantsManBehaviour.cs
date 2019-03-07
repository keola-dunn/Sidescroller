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

    void Awake()
    {
        base.Awake();
        m_dead = false;
        maxSpeed = 3f;
        maxDistance = 8f;
        attackDistance = 4f;

        maxHealth = 100f;
        curHealth = 100f;
        defense = 5;
        attackPower = 10f;
        attackRate = 2f;
        m_FacingRight = true;
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
                m_Anim.SetBool("Run", true);

                moveTowardsPlayer();

            }
            else
            {
                m_Anim.SetBool("Run", false);
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
                            m_Anim.SetBool("Run", true);
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


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        //Flip with transform.localScale = theScale flips the ENTIRE gameObject, including all
        //of it's children. Do we think we can just flip the sprite?
    }

    public void Damage(float[] attr)
    {
        //If defense is greater than or equal to damage taken, 1 damage is taken instead
        if (attr[0] <= (defense - attr[1]))
        {
            curHealth--;
        }
        else
        {
            curHealth -= (attr[0] - Mathf.Max(0, (defense - attr[1])));
        }
        // mHealthBar.ChangeHealth(curHealth, maxHealth);
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