using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBillBoss : EnemyBehaviour
{

    private BoxCollider2D boxCollider;
    private Transform firePoint;
    public GameObject bulletGameObject;
    public bool isAiming;
    public float bulletDamage;
    public float bulletSpeed;
    public float bulletAttackRate;

    // Initialization
    private void Awake()
    {
        base.Awake();
        curHealth = maxHealth;
        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        firePoint = transform.Find("FirePoint");
    }

    // Update is called once per frame
    private void Update()
    {
        if (curHealth <= 0) {
            m_dead = true;
            boxCollider.isTrigger = true;
            m_Rigidbody2D.gravityScale = 0;
            FadeOut(0, 25f);
            Destroy(gameObject, 1f);
        } else {
            float range = Vector2.Distance(transform.position, Player.position);
            if (range > attackDistance && range < maxDistance) {
                if (Time.time > timeToFire)
                {
                    timeToFire = Time.time + 5 / attackRate;
                    // if (isAiming) {
                        // aimAttack();
                    // } else {
                        Attack();
                    // }
                }
            } else if (range <= attackDistance) {
                if (Time.time > timeToFire)
                {
                    timeToFire = Time.time + 2 / attackRate;
                    if (isAiming) {
                        aimAttack();
                    } else {
                        Attack();
                    }
                }
            }
        }
    }

    private void Attack() 
    {
        GameObject generatedBullet = Instantiate(bulletGameObject, firePoint.position, Quaternion.identity);
        BulletBill bulletbill = generatedBullet.GetComponent<BulletBill>();
        bulletbill.Player = this.Player;
        bulletbill.attackPower = bulletDamage;
        bulletbill.attackRate = bulletAttackRate;
        bulletbill.maxSpeed = bulletSpeed;
    }

    private void aimAttack()
    {
        Vector3 difference = Player.position - firePoint.position;
        difference.Normalize();
        float bulletRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        GameObject generatedBullet;
        if (difference.x > 0) {
            generatedBullet = Instantiate(bulletGameObject, firePoint.position, Quaternion.Euler(0, 0, 180f + bulletRotation));
        } else {
            generatedBullet = Instantiate(bulletGameObject, firePoint.position, Quaternion.Euler(0, 0, 180f + bulletRotation));
        }
        BulletBill bulletbill = generatedBullet.GetComponent<BulletBill>();
        bulletbill.Player = this.Player;
        bulletbill.attackPower = bulletDamage;
        bulletbill.attackRate = bulletAttackRate;
        bulletbill.maxSpeed = bulletSpeed;
    }
}
