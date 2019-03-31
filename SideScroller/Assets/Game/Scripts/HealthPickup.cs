using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public float healthBoost = 50f;

    public void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("GetHealthPickup", healthBoost);
            Destroy(this.gameObject);
        }
    }
}
