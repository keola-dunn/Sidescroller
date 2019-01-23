using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 5f;
    public float damage = 10f;
    public LayerMask notToHit; // Determines what you don't want to hit
    public GameObject bullet; // Type of bullet

    private float timeToFire = 0f; // Used to determine when player can shoot
    private Transform firePoint;
    private bool facingRight = true;

    // Initialization
    void Awake()
    {   
        // Find a fire point in the children
        firePoint = transform.Find("FirePoint");
        // If there is no fire point found
        if (firePoint == null) {
            Debug.LogError("NO FIREPOINT FOUND!");
        }
    }

    // FixedUpdate for physics
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0) {
            facingRight = true;
        } 
        if (Input.GetAxis("Horizontal") < 0) {
            facingRight = false;
        }
        // Single burst
        if (fireRate == 0) {
            if (Input.GetButtonDown("Fire1")) {
                Shoot();
            }
        } // Continuous fire
        else {
            // Check time is after the time a shot is available
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 1/fireRate;
                Shoot();
            }
        }
    }

    void Shoot() 
    {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        GameObject shot = Instantiate(bullet, firePointPosition, Quaternion.identity);
        Bullet bulletScript = shot.GetComponent<Bullet>();
        if (facingRight) {
            bulletScript.xDirection = 1f;
        } else {
            bulletScript.xDirection = -1f;
        }
    }
}
