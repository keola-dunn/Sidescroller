using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage= 15;

    //Amount of time between damage-causing contact and when the object can damage player again
    public float simultaneousContactThreshold = 5f;
    private float simulataneousContactCounter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(simulataneousContactCounter < Time.time)
            {
                float[] array = { damage, 0 };
                collision.transform.SendMessage("Damage", array);
                simulataneousContactCounter = Time.time + simultaneousContactThreshold;
            }
        }
        
    }
}
