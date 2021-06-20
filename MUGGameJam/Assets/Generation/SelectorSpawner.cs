using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorSpawner : MonoBehaviour
{
    public GameObject[] possiblePrefabs;
    [SerializeField]
    float[] chances;

    Chunk chunk;

    public bool Spawn(int needToSpawn, int spawnersLeft, int parametr = 0)
    {
        float rand = Random.Range(0.0f, 1.0f);
        float sum = 0;
        for (int i = 0; i < chances.Length; i++)
        {
            sum += chances[i];
            if (rand <= sum)
            {
                if (i < chances.Length - 1)
                {
                    Instantiate(possiblePrefabs[i], transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    return true;
                }
                else
                {
                    if (spawnersLeft >= needToSpawn)
                    {
                        Destroy(gameObject);
                        return false;
                    }
                    else
                    {
                        return Spawn(needToSpawn, spawnersLeft);
                    }
                }

            }
        }
        return false;
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
