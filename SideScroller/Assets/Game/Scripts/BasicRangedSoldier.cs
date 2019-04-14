using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BasicRangedSoldier : RangedSoldier
{
    public int carryOverDefense;
    public float carryOverAttackPower;
    public float carryOverMaxHealth;
    // Start is called before the first frame update
    
    protected override void Awake()
    {
        maxSpeed = 4f;
        maxDistance = 16f;
        attackDistance = 8f;

        maxHealth = 60f;
        defense = 2;
        attackPower = 5f;
        attackRate = 1f;
        isStationary = false;
        base.Awake();
    }
}
