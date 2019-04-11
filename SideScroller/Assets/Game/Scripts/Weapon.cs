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
        // protected bool facingRight = true;

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
            
            ammoDisplay = 
               GameObject.FindGameObjectWithTag("HUD").transform.Find("AmmoDisplay").GetComponent<Text>();

            SetAmmoText(currentAmmo, clipCount * clipSize, false);
        }

        protected void Update() 
        {
            // Rotate the weapon towards the mouse and account for flip
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float weaponRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            if (difference.x < 0) {
                transform.rotation = Quaternion.Euler(0, 180f, 180-weaponRotation);
            } else {
                transform.rotation = Quaternion.Euler(0, 0, weaponRotation);
            }

            if (isReloading) {
                return;
            }
            if (currentAmmo <= 0 || Input.GetButtonDown("Reload")) {
                if (clipCount > 0 && currentAmmo != clipSize) {
                    StartCoroutine(Reload());
                }
                return;
            }
            // Check time is after the time a shot is available
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 10/fireRate;
                Shoot();
            }
        }
        
        /* Firing in 8 directions, called from Platformer2DUserControl.cs, which listens for user input
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
        */

        protected virtual void Shoot() 
        {
            currentAmmo--;
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            GameObject generatedBullet;
            // if (facingRight) {
                generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation);
            // } else {
            //     generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
            // }
            Bullet bulletComponent = generatedBullet.GetComponent<Bullet>();
            bulletComponent.multiplyDamage(damageMultiplier);
            SetAmmoText(currentAmmo, clipSize * clipCount, false);
        }

        protected virtual IEnumerator Reload() {
            isReloading = true;
            SetReloadingText();
            yield return new WaitForSeconds(reloadTime);
            isReloading = false;
            clipCount--;
            currentAmmo = clipSize;
            SetAmmoText(currentAmmo, clipSize * clipCount, false);
        }

        protected void OnEnable() {
            // Switching to current weapon sets isReloading back to false to let weapon fire when reselected
            isReloading = false;
        }

        /* Used to set direction facing from PlatformerCharacter2D, not needed because weapon now faces mouse
        public void setFacingDirection(bool right)
        {
            facingRight = right;
        }
        */

        
        protected void SetAmmoText(int inMagazine, int totalAmmo, bool infiniteAmmo)
        {
            if (infiniteAmmo)
            {
                ammoDisplay.text = inMagazine + " / " + "\u221E";
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

        public void SetAmmoText()
        {
            SetAmmoText(currentAmmo, clipCount * clipSize, clipSize == 12);
        }

        protected void SetReloadingText()
        {
            string sHalf = ammoDisplay.text.Substring(ammoDisplay.text.IndexOf("/"));
            ammoDisplay.text = "reloading " + sHalf;
        }
        
    }
}