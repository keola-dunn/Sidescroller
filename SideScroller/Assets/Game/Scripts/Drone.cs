using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : EnemyBehaviour
{

    private BoxCollider2D boxCollider;
    private Transform firePoint;
    private Transform weapon;
    public GameObject bulletGameObject;

    // Initialization
    void Awake()
    {
        base.Awake();
        maxSpeed = 6.5f;
        maxDistance = 20f;
        attackDistance = 12f;

        maxHealth = 20f;
        curHealth = maxHealth;
        defense = 2;
        attackPower = 3f;
        attackRate = 1f;
        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        weapon = transform.Find("EnemyWeapon");
        firePoint = weapon.Find("FirePoint");
    }

    // Update is called once per frame
    void Update()
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
                if (weapon != null) rotateWeapon();
                moveTowardsPlayer();
            }
            if (range < attackDistance && weapon != null) {
                rotateWeapon();
                if (Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / attackRate;
                    Attack();
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
        generatedBullet.GetComponent<Bullet>().setDamage(attackPower);
    }
}
