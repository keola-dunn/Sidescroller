using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallTurret : EnemyBehaviour
{
    private BoxCollider2D boxCollider;
    private Transform firePoint;
    public GameObject bulletGameObject;
    public float waitTime;
    private float timeToMove;
    private bool inside;
    private Vector2 dest;
    private Vector2 origin;
    public bool panicMode;
    public int numShots;
    public float arcAngle;
    private float increment;
    public Transform[] entrances;

    // Initialization
    protected void Awake()
    {
        base.Awake();
        curHealth = maxHealth;
        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        firePoint = transform.Find("FirePoint");
        panicMode = false;
        increment = arcAngle/numShots;
        origin = transform.position;
        dest = new Vector2(transform.position.x - 2.2f, transform.position.y);
        inside = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (curHealth <= 0) {
            boxCollider.isTrigger = true;
            m_dead = true;
            if (currentFade > 0) {
                currentFade = currentFade - (currentFade / 25f);
                foreach (Transform child in transform.parent) {
                    child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, currentFade);
                }
            }
            Destroy(transform.parent.gameObject, 1f);
        } else {
            float range = Vector2.Distance(transform.position, Player.position);
            if (range <= maxDistance)
            {
                if (!panicMode && curHealth <= maxHealth/2) {
                    panicMode = true;
                    attackRate *= 2.5f;
                    maxSpeed *= 2f;
                    waitTime /= 2f;
                    defense *= 1.5f;
                }
                if (Time.time > timeToMove) {
                    if (Vector2.Distance(transform.position, dest) <= 0.01) {
                        inside = !inside;
                        if (inside) {
                            transform.position = entrances[Random.Range(0, entrances.Length)].position;
                            origin = transform.position;
                            dest = new Vector2(transform.position.x - 2.2f, transform.position.y);
                        } else {
                            Vector2 temp = origin;
                            origin = dest;
                            dest = temp;
                            timeToMove = Time.time + waitTime;
                        }
                    }
                    transform.position = Vector2.MoveTowards(transform.position, dest, maxSpeed * Time.deltaTime);
                } else {
                    if (Time.time > timeToFire) {
                        timeToFire = Time.time + 1 / attackRate;
                        Attack();
                    }
                }
                
            }
        }
    }

    private void Attack()
    {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        Vector3 difference = Player.position - firePoint.position;
        difference.Normalize();
        float bulletRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        GameObject generatedBullet;
        if (difference.x < 0) {
            if (!panicMode) {
                generatedBullet = Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0, 180f, 180f - bulletRotation));
                generatedBullet.GetComponent<Bullet>().setDamage(attackPower);
            } else {
                for (int i = 0; i < numShots; ++i) {
                    float shotDirection = arcAngle/2 - i*increment;
                    generatedBullet = Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0, 180f, 180f - bulletRotation)*Quaternion.Euler(0f, 0f, shotDirection));
                    generatedBullet.GetComponent<Bullet>().setDamage(attackPower);
                }
            }
        } else {
            if (!panicMode) {
                generatedBullet = Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0, 0, bulletRotation));
                generatedBullet.GetComponent<Bullet>().setDamage(attackPower);
            } else {
                for (int i = 0; i < numShots; ++i) {
                    float shotDirection = arcAngle/2 - i*increment;
                    generatedBullet = Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0, 0, bulletRotation)*Quaternion.Euler(0f, 0f, shotDirection));
                    generatedBullet.GetComponent<Bullet>().setDamage(attackPower);
                }
            }
        }
    }
}
