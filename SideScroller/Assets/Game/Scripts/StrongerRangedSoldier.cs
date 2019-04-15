using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongerRangedSoldier : RangedSoldier
{
    // Initialization
    protected override void Awake()
    {
        maxSpeed = 6.5f;
        maxDistance = 20f;
        attackDistance = 18f;
        shootAndMove = false;
        maxHealth = 140f;
        defense = 2;
        attackPower = 8f;
        attackRate = 2.5f;
        isStationary = false;
        isOscillating = false;
        base.Awake();
    }
}
