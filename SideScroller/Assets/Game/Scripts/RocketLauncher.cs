using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wep {
    public class RocketLauncher : Weapon
    {
        // Initialization
        protected override void Awake()
        {
            fireRate = 10f;
            damageMultiplier = 1f;
            clipSize = 2;
            reloadTime = 2.5f;
            base.Awake();
        }
    }
}

