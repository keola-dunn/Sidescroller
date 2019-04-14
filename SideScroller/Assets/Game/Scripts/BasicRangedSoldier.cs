using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedSoldier : RangedSoldier
{
    // Initialization
    protected override void Awake()
    {
        maxSpeed = 4f;
        maxDistance = 16f;
        attackDistance = 12f;

        maxHealth = 60f;
        defense = 2;
        attackPower = 5f;
        attackRate = 1f;
        isStationary = false;
        base.Awake();
    }
}
