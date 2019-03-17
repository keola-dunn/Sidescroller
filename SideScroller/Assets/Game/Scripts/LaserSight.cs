using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{

    private LineRenderer aimSightLine;

    // Initialization
    void Awake()
    {
        aimSightLine = GetComponent<LineRenderer>();
        aimSightLine.widthMultiplier = 0.01f;
        aimSightLine.positionCount = 2;
        aimSightLine.SetColors(Color.red, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.z = 0;
        difference.Normalize();
        aimSightLine.SetPositions(new Vector3[2]{transform.position, transform.position + difference*20});
    }
}
