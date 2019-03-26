using System.Collections;
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

    protected EnemyHealthBar mHealthBar;

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

    public bool FreeYMovement;


    public float currentFade = 1f;


    protected void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }



    //Flips enemy by inverting x, to face direction of player based on m_FacingRight
    protected void flipCheck()
    {
        if (Player.position.x > transform.position.x && m_FacingRight)
        {
            Flip();
        }
        else if (Player.position.x < transform.position.x && !m_FacingRight)
        {
            Flip();
        }
    }



    protected void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        // Vector3 theScale = transform.localScale;
        // theScale.x *= -1;
        // transform.localScale = theScale;
        transform.Rotate(0, 180f, 0);
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
        if(mHealthBar != null)
        {
            mHealthBar.ChangeHealth(curHealth, maxHealth);
        }
    }




    protected void FadeOut(float fadeGoal, float timeToFade)
    {

        if( currentFade > fadeGoal)
        {
           
            currentFade = currentFade - ((currentFade - fadeGoal) / timeToFade);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, currentFade);
        }
    }

    //Moves toward player, at maxSpeed. Will only move in y direction if FreeYMovement
    protected void moveTowardsPlayer()
    {
        Vector3 moveGoal = Player.position;
        if (!FreeYMovement)
        {
            moveGoal.y = transform.position.y;
        }
        transform.position = Vector2.MoveTowards(transform.position, moveGoal, maxSpeed * Time.deltaTime);
    }


}
