using UnityEngine;

namespace Assets.Game.Scripts
{
    class ShotgunAmmo:MonoBehaviour
    {

        private const int ammoCount = 8;

        public void Start()
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //collision.gameObject.SendMessage("GetHealthPickup", ammoCount);
                Destroy(this.gameObject);
            }
        }
    }
}
