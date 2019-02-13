using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wep {
    public class Weapon : MonoBehaviour
    {   
        public GameObject bulletGameObject; // Type of bullet
    
        public float fireRate;
        public float damageMultiplier;
        protected float timeToFire = 0f; // Used to determine when player can shoot
        protected Transform firePoint;
        protected bool facingRight = true;
    
        // Initialization
        protected virtual void Awake()
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
                    if (rightPressed) {
                        transform.rotation = Quaternion.Euler(0, 0, 45);
                    } else {
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                } else if (downPressed) {
                    if (rightPressed) {
                        transform.rotation = Quaternion.Euler(0, 0, -45);
                    } else {
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                    }
                } else {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            } else {
                if (upPressed) {
                    if (leftPressed) {
                        transform.rotation = Quaternion.Euler(0, 0, -45);
                    } else {
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                    }
                } else if (downPressed) {
                    if (leftPressed) {
                        transform.rotation = Quaternion.Euler(0, 0, 45);
                    } else {
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                } else {
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
                    timeToFire = Time.time + 10/fireRate;
                    Shoot();
                }
            }
        }

        protected virtual void Shoot() 
        {
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            GameObject generatedBullet;
            if (facingRight) {
                generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation);
            } else {
                generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
            }
            Bullet bulletComponent = generatedBullet.GetComponent<Bullet>();
            bulletComponent.multiplyDamage(damageMultiplier);
        }

        public void setFacingDirection(bool right)
        {
            facingRight = right;
        }
    }
}