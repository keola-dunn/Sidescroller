using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wep {
    public class RocketLauncher : Weapon
    {
        // Initialization
        protected override void Awake()
        {
            fireRate = 7.5f;
            damageMultiplier = 1f;
            clipSize = 2;
            reloadTime = 4f;
            base.Awake();
        }
    }
}

