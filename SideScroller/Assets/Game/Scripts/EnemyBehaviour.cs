﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    //Generic Animator 
    protected Animator m_Anim;

    // Player who the Enemy is targetting
    public Transform Player;

    //Rigidbody of Enemy
    protected Rigidbody2D m_Rigidbody2D;



    //Max Speed of Enemy 
    protected float maxSpeed;

    //Distance from which the enemy shall begin following player
    public float maxDistance;

    //Distance from which the enemy can attack
    public float attackDistance;

    public float maxHealth;
    public float curHealth;
    public float defense;


    public float attackPower;
    protected float timeToFire;
    protected float attackRate;


    protected bool m_FacingRight;
    public bool m_dead;


    protected void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    protected void Flip()
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
