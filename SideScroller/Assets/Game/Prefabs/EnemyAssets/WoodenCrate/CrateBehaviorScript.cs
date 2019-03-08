using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBehaviorScript : EnemyBehaviour
{



    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        curHealth = 20f;
        defense = 5;
        maxHealth = 20;
}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (curHealth <= 0)
        {
            m_dead = true;
            m_Anim.SetBool("Dead", true);
            Destroy(gameObject, 2f);
        }
    }
}
