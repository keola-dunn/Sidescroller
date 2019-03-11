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
        }

        protected override IEnumerator Reload() {
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            isReloading = false;
            currentAmmo = clipSize;
            // SetAmmoText(currentAmmo, clipSize * clipCount);
        }
    }
}