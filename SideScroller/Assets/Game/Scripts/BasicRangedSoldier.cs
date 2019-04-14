using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BasicRangedSoldier : RangedSoldier
{
<<<<<<< HEAD

    public int carryOverDefense;
    public float carryOverAttackPower;
    public float carryOverMaxHealth;
    // Start is called before the first frame update
    void Awake()
=======
    // Initialization
    protected override void Awake()
>>>>>>> 23ebd14d8700e7cc6e54973e2f3b1fb43cba1aff
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
