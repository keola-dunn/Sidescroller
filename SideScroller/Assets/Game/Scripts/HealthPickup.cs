using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    private float healthBoost = 50f;

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
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(),
                collision.gameObject.GetComponent<BoxCollider2D>(), true);

            throw new System.Exception("Is this even executing?");
        }
       
        
    }
}
