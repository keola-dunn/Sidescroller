using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{

    public Sprite afterBounceSprite;
    public GameObject item;
    private Vector2 originalPosition;
    private bool canBounce;

    // Initialization
    private void Awake()
    {
        canBounce = true;
        originalPosition = transform.parent.localPosition;
    }

    /* Use when background layout is removed
    private IEnumerator Bounce()
    {
        transform.parent.GetComponent<SpriteRenderer>().sprite = afterBounceSprite;
        while (true) {
            transform.parent.localPosition = new Vector2(transform.parent.localPosition.x, transform.parent.localPosition.y + 4f * Time.deltaTime);
            if (transform.parent.localPosition.y >= originalPosition.y + 0.5f) {
                break;
            }
            yield return null;
        }

        while (true) {
            transform.parent.localPosition = new Vector2(transform.parent.localPosition.x, transform.parent.localPosition.y - 4f * Time.deltaTime);
            if (transform.parent.localPosition.y <= originalPosition.y) {
                transform.parent.localPosition = originalPosition;
                break;
            }
            yield return null;
        }
    }
    */

    private void spawnItem()
    {
        GameObject spawnedItem = Instantiate(item, new Vector2(originalPosition.x, originalPosition.y + 1f), Quaternion.identity);
        if (spawnedItem.tag == "Coin") {
            StartCoroutine(spawnedItem.GetComponent<Coin>().Activate());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && canBounce) {
            canBounce = false;
            // StartCoroutine(Bounce());
            spawnItem();
            transform.parent.GetComponent<SpriteRenderer>().sprite = afterBounceSprite;
        }
    }
}
