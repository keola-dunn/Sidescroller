using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : EnemyBehaviour
{

    protected BoxCollider2D mBoxCollider;
    public GameObject mGameObject;
    private bool mSwarming = false;


    // Start is called before the first frame update
    private new void Awake()
    {
        base.Awake();
        maxSpeed = 7f;
        maxDistance = 12f;
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
        RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, Player.position);

        Vector3 moveGoal = Player.position;

        if (!FreeYMovement)
        {
            moveGoal.y = transform.position.y;
        }
        if (mSwarming && lineOfSight.collider.name.Contains("ZombieEnemy"))
        {
            moveGoal.y = Player.position.y + 4;
        }
        transform.position = Vector2.MoveTowards(transform.position, moveGoal, maxSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("ZombieEnemy"))
        {
            mSwarming = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("ZombieEnemy"))
        {
            mSwarming = false;
        }
    }

}
