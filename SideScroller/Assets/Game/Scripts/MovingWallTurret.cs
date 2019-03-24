using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallTurret : EnemyBehaviour
{
    private BoxCollider2D boxCollider;
    private Transform firePoint;
    private float initialYPosition;
    public GameObject bulletGameObject;
    public float verticalMovementRange;
    public float timeToMove;
    public float moveRate;
    public bool panicMode;

    // Initialization
    protected void Awake()
    {
        base.Awake();
        maxSpeed = 8f;
        maxDistance = 14f;

        maxHealth = 500f;
        curHealth = maxHealth;
        defense = 3;
        attackPower = 5f;
        attackRate = 1f;
        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        firePoint = transform.Find("FirePoint");
        initialYPosition = transform.position.y;
        verticalMovementRange = 5;
        moveRate = 3f;
        panicMode = false;
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
            if (curHealth <= maxHealth) {
                panicMode = true;
            }
            if (range <= maxDistance)
            {
                if (Time.time > timeToMove) {
                    if (!panicMode) {
                        timeToMove = Time.time + 10 / moveRate;
                    } else {
                        timeToMove = Time.time + 5 / moveRate;
                    }
                    Vector2 nextPosition = new Vector2(transform.position.x, initialYPosition + Random.Range(-verticalMovementRange, verticalMovementRange));
                    transform.position = Vector2.MoveTowards(transform.position, nextPosition, 2*verticalMovementRange);
                }
                if (Time.time > timeToFire) {
                    timeToFire = Time.time + 1 / attackRate;
                    Attack();
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
            generatedBullet = Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0, 180f, 180f - bulletRotation));
        } else {
            generatedBullet = Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0, 0, bulletRotation));
        }
    }
}
