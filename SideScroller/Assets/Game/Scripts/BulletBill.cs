using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBill : EnemyBehaviour
{

    private BoxCollider2D boxCollider;
    private Transform firePoint;
    private Transform weapon;
    private Rigidbody2D rb;
    public bool lockOn;
    public GameObject bulletGameObject;
    public Sprite explosionEffect;

    // Initialization
    protected void Awake()
    {
        base.Awake();
        m_FacingRight = true;
        curHealth = maxHealth;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        weapon = transform.Find("EnemyWeapon");
        firePoint = weapon.Find("FirePoint");
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        Destroy(gameObject, 30f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (curHealth <= 0) {
            m_dead = true;
            boxCollider.isTrigger = true;
            gameObject.layer = 2;
            weapon.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = explosionEffect;
            Destroy(gameObject, 0.25f);
        } else {
            rb.velocity = -transform.right * maxSpeed;
            float range = Vector2.Distance(transform.position, Player.position);
            if (range > attackDistance && range < maxDistance) {
                rotateWeapon();
            } else {
                
                if (range <= attackDistance)
                {
                    if (lockOn) {
                        rotateSelf();
                        flipCheck();
                    }
                    rotateWeapon();
                    if (Time.time > timeToFire)
                    {
                        timeToFire = Time.time + 1 / attackRate;
                        Attack();
                    }
                    
                }
            }
        }
    }

    private void rotateSelf() {
        Vector3 difference = Player.position - transform.position;
        difference.Normalize();
        float bodyRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (difference.x < 0) {
            transform.rotation = Quaternion.Euler(0, 180f, 180-bodyRotation);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, bodyRotation);
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("here");
        if (col.gameObject.tag == "Player") {
            m_dead = true;
            boxCollider.isTrigger = true;
            gameObject.layer = 2;
            weapon.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = explosionEffect;
            Destroy(gameObject, 0.25f);
            float[] array = { attackPower*2, 0 };
            col.transform.SendMessage("Damage", array);
        } else if (col.gameObject.tag == "SolidPlatform" || col.gameObject.tag == "Enemy") {
            m_dead = true;
            boxCollider.isTrigger = true;
            gameObject.layer = 2;
            weapon.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = explosionEffect;
            Destroy(gameObject, 0.25f);
        }
    }
}
