using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    //Generic Animator 
    private Animator m_Anim;

    // Player who the Enemy is targetting
    public Transform Player;

    //Rigidbody of Enemy
    private Rigidbody2D m_Rigidbody2D;



    //Max Speed of Enemy 
    private float maxSpeed = 2f;

    //Distance from which the enemy shall begin following player
    public float maxDistance = 9f;

    //Distance from which the enemy can attack
    public float attackDistance = 1.65f;


    public float maxHealth = 100f;
    public float curHealth = 100f;
    public float defense = 5;


    public float attackPower = 10f;
    private float timeToFire = 0f; // Used to determine when enemy can Attack
    private float attackRate = 2f;


    private bool m_FacingRight = true;  // For determining which way the player is currently facing.


    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
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
            curHealth -= (attr[0] - Mathf.Max(0, (defense - attr[1])));
        }
    }


}
