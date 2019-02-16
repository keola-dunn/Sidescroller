using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class HealthBar : MonoBehaviour
{

    private Transform healthBar;
    private const float fullBar = 3f;
    private const float verticalBarHeight = 1f;

    private float testVar = 3f;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.Find("Health");
    
        //this is how to change the size of the bar
        //healthBar.localScale = new Vector3(1.5f, verticalBarHeight);
    }


    public void ChangeHealth(float curHealth, float maxHealth)
    {
        healthBar.localScale = 
            new Vector3((curHealth / maxHealth) * fullBar, verticalBarHeight);
    }
}
