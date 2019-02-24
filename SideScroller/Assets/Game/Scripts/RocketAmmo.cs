using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAmmo : MonoBehaviour
{
    private const int ammoCount = 2;

    void Start()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("GetHealthPickup", ammoCount);
            Destroy(this.gameObject);
        }
    }


}
