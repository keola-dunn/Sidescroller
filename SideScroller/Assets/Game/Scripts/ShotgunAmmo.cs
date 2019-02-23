using UnityEngine;

namespace Assets.Game.Scripts
{
    class ShotgunAmmo:MonoBehaviour
    {

        private const int ammoCount = 8;
        private GameObject thisShotgunAmmo;

        public void Start()
        {
            thisShotgunAmmo = this.gameObject;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //collision.gameObject.SendMessage("GetHealthPickup", ammoCount);
                Destroy(thisShotgunAmmo);
            }
        }
    }
}
