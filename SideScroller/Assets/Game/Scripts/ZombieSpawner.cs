using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject mEnemy;
    public GameObject mEnemy2;
    public int spawnCount;
    public int enemy2Frequency;
    public bool isLocationTriggered;


    private Collider2D mColliderTrigger = null;

    void Awake()
    {
        if(enemy2Frequency <= 0 )
        {
            mEnemy2 = null;
        }
        if(mEnemy2 != null && mEnemy == null)
        {
            mEnemy = mEnemy2;
            mEnemy2 = null;
        }
        if(mEnemy == null)
        {
            Destroy(this);
        }
        if (!isLocationTriggered)
        {
            
             SpawnEnemies();
           
        }
        else
        {
            mColliderTrigger = gameObject.GetComponent<Collider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            SpawnEnemies();
            Destroy(this);
        }
    }


    private void SpawnEnemies()
    {
        Vector3 loc = this.transform.position;

        if (mEnemy2 == null)
        {
            for (int i = 0; i < spawnCount; ++i)
            {
                Instantiate(mEnemy, loc, transform.rotation);
            }
        }
        else
        {
            int freq2 = 0;
            for(int i = 0; i<spawnCount; ++i)
            {
                if(freq2 != enemy2Frequency)
                {
                    Instantiate(mEnemy, loc, transform.rotation);
                    ++freq2;
                }
                else
                {
                    Instantiate(mEnemy2, loc, transform.rotation);
                    freq2 = 0;
                }
            }
        }
    }


}
