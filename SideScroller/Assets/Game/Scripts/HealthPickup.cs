using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    private float healthBoost = 25f;

    private GameObject thisHealthBoost;

    public void Start()
    {
        thisHealthBoost = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("getHealthPickup", healthBoost);
            Destroy(thisHealthBoost);
        }
    }
}
