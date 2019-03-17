using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSoldier : EnemyBehaviour
{

    private BoxCollider2D boxCollider;
    private Transform firePoint;
    private Transform weapon;
    public GameObject bulletGameObject;

    // Initialization
    protected void Awake()
    {
        base.Awake();
        maxSpeed = 8f;
        maxDistance = 14f;
        attackDistance = 6f;

        maxHealth = 60f;
        curHealth = 60f;
        defense = 2;
        attackPower = 5f;
        attackRate = 3f;
        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        weapon = transform.Find("EnemyWeapon");
        firePoint = weapon.Find("FirePoint");
    }

    // Update is called once per frame
    private void Update()
    {
        if (curHealth <= 0) {
            boxCollider.isTrigger = true;
            m_dead = true;
            FadeOut(0, 25f);
            Destroy(gameObject, 1f);
        } else {
            float range = Vector2.Distance(transform.position, Player.position);
            if (range > attackDistance && range < maxDistance) {
                moveTowardsPlayer();
            } else {
                rotateWeapon();
                if (range <= attackDistance)
                {
                    if (Time.time > timeToFire)
                    {
                        timeToFire = Time.time + 1 / attackRate;
                        Attack();
                    }
                    
                }
            }
            flipCheck();
        }
    }

    private void rotateWeapon() {
        Vector3 difference = Player.position - transform.position;
        difference.Normalize();
        float weaponRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (difference.x < 0) {
            weapon.rotation = Quaternion.Euler(0, 180f, 180-weaponRotation);
        } else {
            weapon.rotation = Quaternion.Euler(0, 0, weaponRotation);
        }
    }

    private void Attack()
    {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        GameObject generatedBullet = Instantiate(bulletGameObject, firePointPosition, weapon.rotation);
    }
}
