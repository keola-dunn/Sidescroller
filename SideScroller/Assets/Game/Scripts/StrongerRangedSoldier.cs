using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongerRangedSoldier : RangedSoldier
{
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        maxSpeed = 5f;
        maxDistance = 14f;
        attackDistance = 8f;

        maxHealth = 100f;
        curHealth = maxHealth;
        defense = 2;
        attackPower = 8f;
        attackRate = 2f;
    }
}
