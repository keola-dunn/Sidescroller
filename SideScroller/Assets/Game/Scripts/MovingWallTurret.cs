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
    public int numShots;
    public float arcAngle;
    private float increment;
    private Vector2 nextPosition;

    // Initialization
    protected void Awake()
    {
        base.Awake();
        maxSpeed = 9f;
        maxDistance = 15f;
        maxHealth = 500f;
        curHealth = maxHealth;
        defense = 3;
        attackPower = 5f;
        attackRate = 2f;
        m_FacingRight = true;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        firePoint = transform.Find("FirePoint");
        initialYPosition = transform.position.y;
        verticalMovementRange = 6;
        moveRate = 4f;
        panicMode = false;
        numShots = 3;
        arcAngle = 15f;
        increment = arcAngle/numShots;
    }

    // Update is called once per frame
    private void Update()
    {
        if (curHealth <= 0) {
            boxCollider.isTrigger = true;
            m_dead = true;
            FadeOut(0, 25f);
            Destroy(transform.parent.gameObject, 1f);
        } else {
            float range = Vector2.Distance(transform.position, Player.position);
            if (curHealth <= maxHealth/2) {
                panicMode = true;
            }
            if (range <= maxDistance)
            {
                if (Time.time > timeToMove) {
                    if (!panicMode) {
                        timeToMove = Time.time + 10 / moveRate;
                    } else {
                        timeToMove = Time.time + 7 / moveRate;
                    }
                    nextPosition = new Vector2(transform.position.x, initialYPosition + Random.Range(-verticalMovementRange, verticalMovementRange));
                }
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, maxSpeed * Time.deltaTime);
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
        if (difference.x < 0) {
            if (!panicMode) {
                Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0, 180f, 180f - bulletRotation));
            } else {
                for (int i = 0; i < numShots; ++i) {
                    float shotDirection = arcAngle/2 - i*increment;
                    Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0f, 180f, shotDirection));
                }
            }
        } else {
            if (!panicMode) {
                Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0, 0, bulletRotation));
            } else {
                for (int i = 0; i < numShots; ++i) {
                    float shotDirection = arcAngle/2 - i*increment;
                    Instantiate(bulletGameObject, firePointPosition, Quaternion.Euler(0f, 0f, shotDirection));
                }
            }
        }
    }
}
