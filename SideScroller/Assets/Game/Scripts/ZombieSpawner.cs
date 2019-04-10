using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject mEnemy;
    public int spawnCount;

    void Awake()
    {
        if (mEnemy != null)
        {
            Vector3 loc = this.transform.position;
            for (int i = 0; i < spawnCount; ++i)
            {
                Instantiate(mEnemy, loc, transform.rotation);
            }
        }
    }
}
