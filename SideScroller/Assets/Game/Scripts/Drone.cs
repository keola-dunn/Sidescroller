using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : RangedSoldier
{

    // Initialization
    protected override void Awake()
    {
        maxSpeed = 5.5f;
        maxDistance = 20f;
        attackDistance = 12f;

        maxHealth = 35f;
        defense = 2;
        attackPower = 3f;
        attackRate = 1f;
        isStationary = false;
        isOscillating = false;
        FreeYMovement = true;
        base.Awake();
    }

    protected override void nonStationaryUpdate() 
    {
        if (curHealth <= 0) {
            m_dead = true;
            boxCollider.isTrigger = true;
            m_Rigidbody2D.velocity = Vector2.zero;
            FadeOut(0, 25f);
            Destroy(gameObject, 1f);
        } else {
            float range = Vector2.Distance(transform.position, Player.position);
            if (range < maxDistance) {
                rotateWeapon();
                moveTowardsPlayer();
            } 
                
            if (range <= attackDistance)
            {
                if (Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / attackRate;
                    Attack();
                }
                    
            }
            flipCheck();
        }
    }
}
