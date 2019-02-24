using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private Transform healthBar;
    private const float fullBar = 0.6f;
    private const float verticalBarHeight = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.Find("Health");
    }


    public void ChangeHealth(float curHealth, float maxHealth)
    {
        if (curHealth > 0)
        {
            healthBar.localScale =
                new Vector3((curHealth / maxHealth) * fullBar, verticalBarHeight);
        }
        else
        {
            healthBar.localScale = new Vector3(0f, verticalBarHeight);
        }
    }
}
