using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowser : EnemyBehaviour
{

    public float firePower;
    public float fireRate;
    public int numShots;
    public float arcAngle;
    private float increment;
    public float bulletBillRate;
    public float bulletBillDamage;
    public float bulletBillSpeed;
    public float bulletBillAttackRate;
    public GameObject bulletGameObject;
    public GameObject fireGameObject;
    public GameObject bulletBillGameObject;
    protected BoxCollider2D boxCollider;
    protected Transform weaponFirePoint;
    protected Transform weapon;
    protected Transform mouthFirePoint;
    protected Transform cannonFirePoint;
    private float timeToShootGun;
    private float timeToShootCannon;

    // Initialization
    private void Awake()
    {
        base.Awake();
        m_FacingRight = true;
        curHealth = maxHealth;
        mHealthBar = this.transform.Find("EnemyHealthCanvas").GetComponent<EnemyHealthBar>();
        boxCollider = GetComponent<BoxCollider2D>();
        weapon = transform.Find("EnemyWeapon");
        weaponFirePoint = weapon.Find("FirePoint");
        mouthFirePoint = transform.Find("FirePoint");
        cannonFirePoint = transform.Find("BulletBillCannon").Find("FirePoint");
        increment = arcAngle/numShots;
    }

    // Update is called once per frame
    private void Update()
    {
        if (curHealth <= 0) {
            m_dead = true;
            boxCollider.isTrigger = true;
            gameObject.layer = 2;
            m_Rigidbody2D.gravityScale = 0;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")) {
                FadeOut(obj, 0, 25f);
                Destroy(obj, 1f);
            }
        } else {
            float range = Vector2.Distance(transform.position, Player.position);
            if (range < maxDistance) {
                rotateWeapon();
                if (range >= attackDistance) moveTowardsPlayer();
                if (Time.time > timeToShootGun)
                {
                    timeToShootGun = Time.time + 1 / attackRate;
                    gunAttack();
                }
                if (Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / fireRate;
                    mouthAttack();
                }
                if (Time.time > timeToShootCannon)
                {
                    timeToShootCannon = Time.time + 1 / bulletBillRate;
                    cannonAttack();
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

    private void gunAttack()
    {
        Vector2 firePointPosition = new Vector2(weaponFirePoint.position.x, weaponFirePoint.position.y);
        GameObject generatedBullet = Instantiate(bulletGameObject, firePointPosition, weapon.rotation);
        generatedBullet.GetComponent<Bullet>().setDamage(attackPower);
    }

    private void mouthAttack()
    {
        Vector2 firePointPosition = new Vector2(mouthFirePoint.position.x, mouthFirePoint.position.y);
        Vector3 difference = Player.position - mouthFirePoint.position;
        difference.Normalize();
        float bulletRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        GameObject generatedBullet;
        if (difference.x < 0) {
            for (int i = 0; i < numShots; ++i) {
                float shotDirection = arcAngle/2 - i*increment;
                generatedBullet = Instantiate(fireGameObject, firePointPosition, Quaternion.Euler(0, 180f, 180f - bulletRotation)*Quaternion.Euler(0f, 0f, shotDirection));
                generatedBullet.GetComponent<Bullet>().setDamage(firePower);
            }
        } else {
            for (int i = 0; i < numShots; ++i) {
                float shotDirection = arcAngle/2 - i*increment;
                generatedBullet = Instantiate(fireGameObject, firePointPosition, Quaternion.Euler(0, 0, bulletRotation)*Quaternion.Euler(0f, 0f, shotDirection));
                generatedBullet.GetComponent<Bullet>().setDamage(firePower);
            }
        }
    }

    private void cannonAttack()
    {
        Vector3 difference = cannonFirePoint.position - Player.position;
        difference.Normalize();
        float bulletRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        GameObject generatedBullet;
        if (difference.x < 0) {
            generatedBullet = Instantiate(bulletBillGameObject, cannonFirePoint.position, Quaternion.Euler(0, 180f, 180f - bulletRotation));
        } else {
            generatedBullet = Instantiate(bulletBillGameObject, cannonFirePoint.position, Quaternion.Euler(0, 0, bulletRotation));
        }
        BulletBill bulletbill = generatedBullet.GetComponent<BulletBill>();
        bulletbill.Player = this.Player;
        bulletbill.attackPower = bulletBillDamage;
        bulletbill.attackRate = bulletBillAttackRate;
        bulletbill.maxSpeed = bulletBillSpeed;
        Physics2D.IgnoreCollision(boxCollider, generatedBullet.GetComponent<BoxCollider2D>());
    }

    private void FadeOut(GameObject target, float fadeGoal, float timeToFade)
    {
        if( currentFade > fadeGoal)
        {
            currentFade = currentFade - ((currentFade - fadeGoal) / timeToFade);
            target.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, currentFade);
        }
    }
}
