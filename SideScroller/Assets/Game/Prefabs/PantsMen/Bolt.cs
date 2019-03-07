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
        protected bool isReloading = false;
        protected float timeToFire = 0f; // Used to determine when player can shoot
        public Transform firePoint;
     



        protected void Awake()
        {
            damageMultiplier = 2f;
            clipSize = 2;
            reloadTime = 10f;


        // Find a fire point in the children
        firePoint = transform.Find("FirePoint");
        // If there is no fire point found
        if (firePoint == null)
        {
            Debug.LogError("NO FIREPOINT FOUND!");
        }
    }








    public void Shoot()
    {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        GameObject generatedBullet;
        generatedBullet = Instantiate(bulletGameObject, firePointPosition, transform.rotation);


        BoltShot bulletComponent = generatedBullet.GetComponent<BoltShot>();
        bulletComponent.multiplyDamage(damageMultiplier);
    }




}