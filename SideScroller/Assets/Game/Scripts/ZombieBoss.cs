using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBoss : EnemyBehaviour
{
    private bool mSwarming = false;
    public int mLevel;
    private static GameObject boss3;
    private static GameObject boss2;
    private static GameObject boss1;
    private static GameObject boss0;

    private bool airborne = false;


    // Start is called before the first frame update
    private new void Awake()
    {
        boss3 = Resources.Load("ZombieBoss3") as GameObject;
        boss2 = Resources.Load("ZombieBoss2") as GameObject;
        boss1 = Resources.Load("ZombieBoss1") as GameObject;
        boss0 = Resources.Load("ZombieBoss0") as GameObject;

        Player = GameObject.Find("CharacterRobotBoy").transform;

        base.Awake();
        
        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth <= 0)
        {
            if (!m_dead)
            {
                SpawnSubZombies();
            }
            m_dead = true;
            m_Rigidbody2D.velocity = Vector2.zero;
            FadeOut(0, 5f);
            Destroy(gameObject, 0.5f);
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
            m_Rigidbody2D.AddForce(new Vector2(0, 90), ForceMode2D.Impulse);
        }
        if((moveGoal.y - transform.position.y < 2) && !airborne)
        {
            switch (mLevel)
            {
                case 4:
                    m_Rigidbody2D.AddForce(new Vector2(0, 2500), ForceMode2D.Impulse);
                    break;
                case 3:
                    m_Rigidbody2D.AddForce(new Vector2(0, 1250), ForceMode2D.Impulse);
                    break;
                case 2:
                    m_Rigidbody2D.AddForce(new Vector2(0, 900), ForceMode2D.Impulse);
                    break;
                case 1:
                    m_Rigidbody2D.AddForce(new Vector2(0, 500), ForceMode2D.Impulse);
                    break;
                case 0:
                    m_Rigidbody2D.AddForce(new Vector2(0, 250), ForceMode2D.Impulse);
                    break;

            }
            airborne = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, moveGoal, maxSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("ZombieEnemy"))
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

    private void SpawnSubZombies()
    {
        if (mLevel != 0) {

            GameObject childZombie = null;

            switch (mLevel)
            {
                case 4:
                    childZombie = boss3;
                    break;
                case 3:
                    childZombie = boss2;
                    break;
                case 2:
                    childZombie = boss1;
                    break;
                case 1:
                    childZombie = boss0;
                    break;
            }

            for(int i = 0; i < 2; ++i)
            {
                Instantiate(childZombie,
                    new Vector3(transform.position.x, 
                    transform.position.y),Quaternion.identity);
            }
        }

    }
}
