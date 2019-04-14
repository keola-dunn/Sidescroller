using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSoldier : EnemyBehaviour
{

    protected BoxCollider2D boxCollider;
    protected Transform firePoint;
    protected Transform weapon;
    public GameObject bulletGameObject;
    public bool isStationary;
    public LayerMask toHit;
    public bool isOscillating;
    private Transform destination;
    private Vector2 dest;
    private Vector2 origin;
    public float waitTime;
    private float timeToMove;

    // Initialization
    protected virtual void Awake()
    {
        base.Awake();
        m_FacingRight = true;
        curHealth = maxHealth;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        weapon = transform.Find("EnemyWeapon");
        firePoint = weapon.Find("FirePoint");
        destination = transform.Find("Destination");
        if (destination) {
            origin = transform.position;
            dest = destination.position;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isStationary) {
            stationaryUpdate();
        } else if (isOscillating) {
            oscillatingUpdate();
        } else {
            nonStationaryUpdate();
        }
    }

    protected virtual void nonStationaryUpdate()
    {
        if (curHealth <= 0) {
            m_dead = true;
            boxCollider.isTrigger = true;
            m_Rigidbody2D.velocity = Vector2.zero;
            FadeOut(0, 25f);
            Destroy(gameObject, 1f);
        } else {
            float range = Vector2.Distance(transform.position, Player.position);
            if (range > attackDistance && range < maxDistance) {
                rotateWeapon();
                moveTowardsPlayer();
            } else {
                
                if (range <= attackDistance)
                {
                    rotateWeapon();
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

    protected virtual void stationaryUpdate()
    {
        if (curHealth <= 0) {
            m_dead = true;
            boxCollider.isTrigger = true;
            m_Rigidbody2D.velocity = Vector2.zero;
            FadeOut(0, 25f);
            Destroy(gameObject, 1f);
        } else {
            float range = Vector2.Distance(transform.position, Player.position);
            if (range <= maxDistance) {
                Vector2 playerPosition = new Vector2(Player.position.x, Player.position.y);
                Vector2 curPosition = new Vector2(transform.position.x, transform.position.y);
                RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, playerPosition - curPosition, maxDistance, toHit);
                if (hitInfo) {
                    if (hitInfo.transform.gameObject.tag == "Player") {
                        rotateWeapon();
                        if (Time.time > timeToFire)
                        {
                            timeToFire = Time.time + 1 / attackRate;
                            Attack();
                        }
                    }
                }
                
            }
            flipCheck();
        }
    }
    
    protected virtual void oscillatingUpdate()
    {
        if (curHealth <= 0) {
            m_dead = true;
            boxCollider.isTrigger = true;
            m_Rigidbody2D.velocity = Vector2.zero;
            FadeOut(0, 25f);
            Destroy(gameObject, 1f);
        } else {
            if (Time.time > timeToMove) {
                if (Vector2.Distance(transform.position, dest) <= 0.01) {
                    Vector2 temp = origin;
                    origin = dest;
                    dest = temp;
                    timeToMove = Time.time + waitTime;
                }
                transform.position = Vector2.MoveTowards(transform.position, dest, maxSpeed * Time.deltaTime);
            }
            float range = Vector2.Distance(transform.position, Player.position);
            if (range <= maxDistance) {
                Vector2 playerPosition = new Vector2(Player.position.x, Player.position.y);
                Vector2 curPosition = new Vector2(transform.position.x, transform.position.y);
                RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, playerPosition - curPosition, maxDistance, toHit);
                if (hitInfo) {
                    if (hitInfo.transform.gameObject.tag == "Player") {
                        rotateWeapon();
                        if (Time.time > timeToFire)
                        {
                            timeToFire = Time.time + 1 / attackRate;
                            Attack();
                        }
                    }
                }
                
            }
            flipCheck();
        }
    }

    protected void rotateWeapon() {
        Vector3 difference = Player.position - transform.position;
        difference.Normalize();
        float weaponRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (difference.x < 0) {
            weapon.rotation = Quaternion.Euler(0, 180f, 180-weaponRotation);
        } else {
            weapon.rotation = Quaternion.Euler(0, 0, weaponRotation);
        }
    }

    protected virtual void Attack()
    {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        GameObject generatedBullet = Instantiate(bulletGameObject, firePointPosition, weapon.rotation);
        generatedBullet.GetComponent<Bullet>().setDamage(attackPower);
    }
}
