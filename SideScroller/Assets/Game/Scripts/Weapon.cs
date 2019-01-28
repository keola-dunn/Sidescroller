using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wep {
    public class Weapon : MonoBehaviour
    {
        
        public float fireRate = 5f;
        public float damage = 10f;
        public GameObject bulletGameObject; // Type of bullet
    
        private float timeToFire = 0f; // Used to determine when player can shoot
        private Transform firePoint;
        private bool facingRight = true;
        private float xShotDirection = 0f;
        private float yShotDirection = 0f;
    
        // Initialization
        private void Awake()
        {   
            // Find a fire point in the children
            firePoint = transform.Find("FirePoint");
            // If there is no fire point found
            if (firePoint == null) {
                Debug.LogError("NO FIREPOINT FOUND!");
            }
        }
        
        public void Action(bool firing, bool upPressed, bool downPressed, bool rightPressed, bool leftPressed)
        {
            // First determine which direction weapon is facing
            if (!facingRight && rightPressed && !leftPressed) {
                facingRight = true;
            } 
            else if (facingRight && !rightPressed && leftPressed) {
                facingRight = false;
            }

            // Angle the weapon appropriately
            if (facingRight) {
                if (upPressed) {
                    yShotDirection = 1f;
                    if (rightPressed) {
                        xShotDirection = 1f;
                        transform.rotation = Quaternion.Euler(0, 0, 45);
                    } else {
                        xShotDirection = 0f;
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                } else if (downPressed) {
                    yShotDirection = -1f;
                    if (rightPressed) {
                        xShotDirection = 1f;
                        transform.rotation = Quaternion.Euler(0, 0, -45);
                    } else {
                        xShotDirection = 0f;
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                    }
                } else {
                    xShotDirection = 1f;
                    yShotDirection = 0f;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            } else {
                if (upPressed) {
                    yShotDirection = 1f;
                    if (leftPressed) {
                        xShotDirection = -1f;
                        transform.rotation = Quaternion.Euler(0, 0, -45);
                    } else {
                        xShotDirection = 0f;
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                    }
                } else if (downPressed) {
                    yShotDirection = -1f;
                    if (leftPressed) {
                        xShotDirection = -1f;
                        transform.rotation = Quaternion.Euler(0, 0, 45);
                    } else {
                        xShotDirection = 0f;
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                } else {
                    xShotDirection = -1f;
                    yShotDirection = 0f;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
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
    
        private void Shoot() 
        {
            // fix bullet spawning slightly shifted position when facing left
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            GameObject generatedBullet = Instantiate(bulletGameObject, firePointPosition, Quaternion.identity);
            Bullet bulletScript = generatedBullet.GetComponent<Bullet>();
            bulletScript.damage += damage;
            bulletScript.xDirection = xShotDirection;
            bulletScript.yDirection = yShotDirection;
        }
    }
}