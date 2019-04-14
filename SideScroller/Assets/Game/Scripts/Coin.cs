using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private bool isActive;
    private Vector2 originalPosition;

    // Initialization 
    private void Awake()
    {
        originalPosition = transform.localPosition;
        isActive = false;
    }

    public IEnumerator Activate()
    {
        isActive = true;
        while (true) {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 4f * Time.deltaTime);
            if (transform.localPosition.y >= originalPosition.y + 1.5f) {
                break;
            }
            yield return null;
        }
        float timeToWait = Time.time + 0.5f;
        while (true) {
            if (Time.time > timeToWait) {
                Debug.Log("Here");
                break;
            }
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (!isActive && col.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
