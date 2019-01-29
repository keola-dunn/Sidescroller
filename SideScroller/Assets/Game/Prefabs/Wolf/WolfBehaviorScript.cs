using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class WolfBehaviorScript : MonoBehaviour
{

    
    private Animator m_Anim;
    private float m_maxSpeed = 2f;
    private Rigidbody2D m_Rigidbody2D;
    public Transform Player;
    public float attackDistance = 1.65f;
    public float maxDistance = 9f;

    public bool m_dead = false;
    public float curHealth = 100f;
    public float defense = 5;
    public float maxHealth = 100f;

    public float attackPower = 10f;
    private float timeToFire = 0f; // Used to determine when enemy can Attack
    private float attackRate = 2f;


    private bool m_FacingRight = true;  // For determining which way the player is currently facing.


    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (curHealth <= 0)
        {
            m_dead = true;
            m_Anim.SetBool("Dead", true);
        }
        else
        {
             float range = Vector2.Distance(transform.position, Player.position);

            if (range > attackDistance && range < maxDistance) {
                m_Anim.SetBool("Run", true);
                transform.position = Vector2.MoveTowards(transform.position, Player.position, m_maxSpeed * Time.deltaTime);
                
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





            //flipping wolf character to follow
            if (Player.position.x > transform.position.x && m_FacingRight)
            {
                Flip();
            } else if(Player.position.x < transform.position.x && !m_FacingRight)
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
            curHealth -= (attr[0] - Mathf.Max(0,(defense - attr[1])));
        }
    }
}




