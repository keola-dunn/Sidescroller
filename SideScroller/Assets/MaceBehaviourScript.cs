using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MaceBehaviourScript : EnemyBehaviour
{
    //Object will move DOWN this much (or up, if value is negative), default is 2
    public float yRange = 2f;

    public float max;
    public float min;


    private new void Awake()
    {

        max = transform.position.y;
        min = max - yRange;
        base.Awake();
        maxSpeed = 4f;
        maxDistance = 10f;
        attackDistance = 1.2f;

        maxHealth = 300;
        curHealth = 300;
        defense = 20;
        attackPower = 30;
        attackRate = 3f;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (curHealth <= 0)
        { 
            m_dead = true;
            FadeOut(0f, 80f);
            Destroy(gameObject, 3f);
        }
        else
        {
            float range = Vector2.Distance(transform.position, Player.position);
            if (range > attackDistance && range < maxDistance)
            {
                m_Anim.SetBool("Awake", true);
                
            
                transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z);



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
                    m_Anim.SetBool("Awake", false);
                }
            }
        }
    }

}




