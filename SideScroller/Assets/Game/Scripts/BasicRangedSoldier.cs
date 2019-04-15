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
        maxSpeed = 6f;
        maxDistance = 20f;
        attackDistance = 16f;
        shootAndMove = true;
        maxHealth = 60f;
        defense = 2;
        attackPower = 5f;
        attackRate = 1f;
        isStationary = false;
        base.Awake();
    }
}
