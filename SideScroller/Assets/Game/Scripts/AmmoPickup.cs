using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    public bool bulletAmmo = false;
    public bool shotgunAmmo = false;
    public bool rocketAmmo = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            bool[] ammoType = { bulletAmmo, shotgunAmmo, rocketAmmo };

            collision.gameObject.SendMessage("AddAmmo", ammoType);
            Destroy(this.gameObject);
        }
    }

}
