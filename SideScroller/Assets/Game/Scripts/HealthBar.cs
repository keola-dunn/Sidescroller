using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class HealthBar : MonoBehaviour
{

    private Transform healthBar;
    private float testVar = 3f;
    private const float verticalBarHeight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.Find("Health");
        //healthBar.localScale = new Vector3(1.5f, verticalBarHeight);
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (testVar > 0)
        {
            testVar = testVar - 0.01f;
            healthBar.localScale = new Vector3(testVar, verticalBarHeight);
        }
    }
}
