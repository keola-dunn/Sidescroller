using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BasicRangedSoldier : RangedSoldier
{

    public int carryOverDefense;
    public float carryOverAttackPower;
    public float carryOverMaxHealth;
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        maxSpeed = 4f;
        maxDistance = 18f;
        attackDistance = 8f;

        maxHealth = 60f;
        curHealth = maxHealth;
        defense = 2;
        attackPower = 5f;
        attackRate = 1f;
    }
}
