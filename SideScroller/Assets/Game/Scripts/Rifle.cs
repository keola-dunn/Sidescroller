﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wep {
    public class Rifle : Weapon
    {

        public float spreadAngle;

        // Initialization
        protected override void Awake()
        {
            fireRate = 150f;
            damageMultiplier = 0.8f;
            spreadAngle = 3f;
            clipSize = 30;
            reloadTime = 1.5f;
            base.Awake();
        }

        protected override void Shoot()
        {
            currentAmmo--;
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            GameObject generatedBullet;
            // if (facingRight) {
                generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation * Quaternion.Euler(0f, 0f, Random.Range(spreadAngle, -spreadAngle)));
            // } else {
            //     generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation * Quaternion.Euler(0f, 0f, 180f + Random.Range(spreadAngle, -spreadAngle)));
            // }
            Bullet bulletComponent = generatedBullet.GetComponent<Bullet>();
            bulletComponent.multiplyDamage(damageMultiplier);
            SetAmmoText(currentAmmo, clipSize * clipCount, false);
        }

    }
}