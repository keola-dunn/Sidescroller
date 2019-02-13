using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wep {
    public class RocketLauncher : Weapon
    {
        // Start is called before the first frame update
        protected override void Awake()
        {
            fireRate = 7.5f;
            damageMultiplier = 1f;
            base.Awake();
        }
    }
}

