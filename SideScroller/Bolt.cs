using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Bolt : MonoBehaviour
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
        public Transform firePoint;
        // protected bool facingRight = true;



        protected override void Awake()
        {
            fireRate = 12f;
            damageMultiplier = 2f;
            clipSize = 2;
            reloadTime = 10f;
            currentAmmo = 9000;
        }



    }