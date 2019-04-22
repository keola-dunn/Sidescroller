using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{

    public float damage;
    private float timeToDamage;
    public float damageRate;

    // Initialization
    private void Awake() {}

    private void OnTriggerStay2D(Collider2D col) 
    {
        if (Time.time > timeToDamage) {
            if (col.tag == "Player") {
                float[] array = { damage, 0 };
                col.transform.SendMessage("Damage", array);
            }
            timeToDamage = Time.time + 1 / damageRate;
        }
    }
}
