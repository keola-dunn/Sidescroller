﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : Bullet
{
    // Initialization
    protected override void Awake()
    {
        damage = 10f;
        speed = 20f;
        defensePenetration = 1f;
        base.Awake();
    }

}