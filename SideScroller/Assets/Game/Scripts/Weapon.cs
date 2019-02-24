using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wep {
    public class Weapon : MonoBehaviour
    {   
        public GameObject bulletGameObject; // Type of bullet
    
        public float fireRate;
        public float damageMultiplier;
        public int clipSize;
        public int clipCount = 10;
        public float reloadTime;
        protected int currentAmmo;
        protected bool isReloading = false;
        protected float timeToFire = 0f; // Used to determine when player can shoot
        protected Transform firePoint;
        protected bool facingRight = true;

        private Text ammoDisplay;

        // Initialization
        protected virtual void Awake()
        {   
            // Find a fire point in the children
            firePoint = transform.Find("FirePoint");
            // If there is no fire point found
            if (firePoint == null) {
                Debug.LogError("NO FIREPOINT FOUND!");
            }
            currentAmmo = clipSize;

            ammoDisplay = Transform.FindObjectOfType<Text>();
            SetAmmoText(currentAmmo, clipCount * clipSize);
        }
        
        public void Action(bool firing, bool reloading, bool upPressed, bool downPressed, bool rightPressed, bool leftPressed)
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

            if (isReloading) {
                return;
            }
            if (currentAmmo <= 0 || reloading) {
                if (clipCount > 0) {
                    StartCoroutine(Reload());
                }
                return;
            }
            // Check time is after the time a shot is available
            if (firing && Time.time > timeToFire) {
                timeToFire = Time.time + 10/fireRate;
                Shoot();
            }
        }

        protected virtual void Shoot() 
        {
            currentAmmo--;
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            GameObject generatedBullet;
            if (facingRight) {
                generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation);
            } else {
                generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
            }
            Bullet bulletComponent = generatedBullet.GetComponent<Bullet>();
            bulletComponent.multiplyDamage(damageMultiplier);
            SetAmmoText(currentAmmo, clipSize * clipCount);
        }

        protected virtual IEnumerator Reload() {
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            isReloading = false;
            clipCount--;
            currentAmmo = clipSize;
            SetAmmoText(currentAmmo, clipSize * clipCount);
        }

        protected void OnEnable() {
            // Switching to current weapon sets isReloading back to false
            isReloading = false;
        }

        public void setFacingDirection(bool right)
        {
            facingRight = right;
        }

        protected void SetAmmoText(int inMagazine, int totalAmmo)
        {
            if (totalAmmo.Equals(int.MaxValue))
            {
                ammoDisplay.text = inMagazine + " / " + ((char)236);
            }
            else if(totalAmmo < 100)
            {
                if (totalAmmo < 10)
                {
                    ammoDisplay.text = "0" + inMagazine + " / 00" + totalAmmo;
                }
                else
                {
                    ammoDisplay.text = "0" + inMagazine + " / 0" + totalAmmo;
                }
            }
            else
            {
                ammoDisplay.text = "0" + inMagazine + " / " + totalAmmo;
            }
        }
    }
}