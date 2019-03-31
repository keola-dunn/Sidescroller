using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wep {
    public class Pistol : Weapon
    {

        // Initialization
        protected override void Awake()
        {   
            fireRate = 50f;
            damageMultiplier = 1f;
            clipSize = 12;
            reloadTime = 0.8f;
            base.Awake();
            SetAmmoText(currentAmmo, clipCount * clipSize, true);
        }

        protected override IEnumerator Reload() {
            isReloading = true;
            SetReloadingText();
            yield return new WaitForSeconds(reloadTime);
            isReloading = false;
            currentAmmo = clipSize;
            SetAmmoText(currentAmmo, clipSize * clipCount, true);
        }

        protected override void Shoot()
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
            SetAmmoText(currentAmmo, clipSize * clipCount, true);
        }
    }
}