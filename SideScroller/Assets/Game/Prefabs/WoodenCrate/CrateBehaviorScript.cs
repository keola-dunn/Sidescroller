using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBehaviorScript : MonoBehaviour
{
    public bool m_dead = false;
    public float curHealth = 20f;
    public float defense = 5;
    public float maxHealth = 20;
    private Animator m_Anim;


    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
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

    public void Damage(float[] attr)
    {
        //If defense is greater than or equal to damage taken, 1 damage is taken instead
        if (attr[0] <= (defense - attr[1]))
        {
            curHealth--;
        }
        else
        {
            print(attr[1]);
            curHealth -= (attr[0] - Mathf.Max(0, (defense - attr[1])));
        }
    }
}
