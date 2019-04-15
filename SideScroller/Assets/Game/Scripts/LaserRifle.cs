using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wep {
    public class LaserRifle : Weapon
    {

        public LayerMask toHit;
        public float damage;
        public LineRenderer laser;

        // Initialization
        protected override void Awake()
        {
            fireRate = 120f;
            damage = 8f;
            clipSize = 30;
            reloadTime = 1f;
            clipCount = 1;
            base.Awake();
        }

        protected override void Update() 
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
                StartCoroutine(ShootLaser());
            }
        }

        protected IEnumerator ShootLaser() 
        {
            currentAmmo--;
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 diff = mousePosition - firePointPosition;
            RaycastHit2D hitInfo = Physics2D.Raycast(firePointPosition, diff, 17f, toHit);
            if (hitInfo) {
                if (hitInfo.transform.gameObject.tag == "Enemy") {
                    float[] array = { damage, 1f };
                    hitInfo.transform.SendMessage("Damage", array);
                }
                laser.SetPosition(0, firePointPosition);
                laser.SetPosition(1, hitInfo.point);
            } else {
                laser.SetPosition(0, firePointPosition);
                laser.SetPosition(1, firePointPosition + 17*diff.normalized);
            }
            laser.enabled = true;
            SetAmmoText(currentAmmo, clipSize * clipCount, false);
            yield return new WaitForSeconds(0.01f);
            laser.enabled = false;
        }

        protected override IEnumerator Reload() {
            isReloading = true;
            SetReloadingText();
            yield return new WaitForSeconds(reloadTime);
            isReloading = false;
            currentAmmo = clipSize;
            SetAmmoText(currentAmmo, clipSize * clipCount, false);
        }
    }
}
