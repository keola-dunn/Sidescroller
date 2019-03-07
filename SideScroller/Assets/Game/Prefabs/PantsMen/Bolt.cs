using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Bolt : MonoBehaviour
    {

        public GameObject bulletGameObject; // Type of bullet
    
        public float damageMultiplier;
        public int clipSize;
        public int clipCount = 10;
        public float reloadTime;
        protected int currentAmmo;
        protected bool isReloading = false;
        protected float timeToFire = 0f; // Used to determine when player can shoot
        public Transform firePoint;
        // protected bool facingRight = true;



        protected void Awake()
        {
            damageMultiplier = 2f;
            clipSize = 2;
            reloadTime = 10f;
            currentAmmo = 9000;
        }



    public void Shoot()
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
        BoltShot bulletComponent = generatedBullet.GetComponent<BoltShot>();
        bulletComponent.multiplyDamage(damageMultiplier);
    }




}