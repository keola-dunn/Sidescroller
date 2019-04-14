using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongerRangedSoldier : RangedSoldier
{
    // Initialization
    protected override void Awake()
    {
        maxSpeed = 5.5f;
        maxDistance = 18f;
        attackDistance = 8f;

        maxHealth = 140f;
        defense = 2;
        attackPower = 8f;
        attackRate = 2f;
        isStationary = false;
        isOscillating = false;
        base.Awake();
    }
}
