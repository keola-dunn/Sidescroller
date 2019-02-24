using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    private float healthBoost = 50f;

    public void Start()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("GetHealthPickup", healthBoost);
            Destroy(this.gameObject);
        }
    }
}
