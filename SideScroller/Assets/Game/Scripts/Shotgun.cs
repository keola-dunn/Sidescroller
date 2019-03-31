using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wep {
    public class Shotgun : Weapon
    {
    
        public int numShots;
        public float arcAngle;
        private float increment;

        // Initialization
        protected override void Awake()
        {   
            fireRate = 15f;
            damageMultiplier = 0.7f;
            clipSize = 5;
            reloadTime = 1.5f;
            numShots = 5;
            arcAngle = 20f;
            increment = arcAngle/numShots;
            base.Awake();
        }

        protected override void Shoot()
        {
            currentAmmo--;
            // if (facingRight) {
                for (int i = 0; i < numShots; ++i) {
                    Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
                    float shotDirection = arcAngle/2 - i*increment;
                    GameObject generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation * Quaternion.Euler(0f, 0f, shotDirection));
                    Bullet bulletComponent = generatedBullet.GetComponent<Bullet>();
                    bulletComponent.multiplyDamage(damageMultiplier);
                }
            SetAmmoText(currentAmmo, clipSize * clipCount, false);

            // } else {
            //     for (int i = 0; i < numShots; ++i) {
            //         Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            //         float flipAdjustedShotDirection = 180f + arcAngle/2 - i*increment;
            //         GameObject generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation * Quaternion.Euler(0f, 0f, flipAdjustedShotDirection));
            //         Bullet bulletComponent = generatedBullet.GetComponent<Bullet>();
            //         bulletComponent.multiplyDamage(damageMultiplier);
            //     }
            // }
        }
    }
}