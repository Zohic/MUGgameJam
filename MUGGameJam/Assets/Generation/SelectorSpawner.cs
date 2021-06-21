using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorSpawner : MonoBehaviour
{
    public GameObject[] possiblePrefabs;
    [SerializeField]
    float[] chances;

    public Chunk chunk;

    public bool spawnOnStart;
    public bool Spawn(int needToSpawn, int spawnersLeft, out GameObject spawned)
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
                    spawned = Instantiate(possiblePrefabs[i], transform.position, Quaternion.identity, chunk.transform);
                    Destroy(gameObject);
                    return true;
                }
                else
                {
                    if (spawnersLeft >= needToSpawn)
                    {
                        spawned = null;
                        Destroy(gameObject);
                        return false;
                    }
                    else
                    {
                        return Spawn(needToSpawn, spawnersLeft, out spawned);
                    }
                }

            }
        }
        spawned = null;
        return false;
    }

    void Start()
    {
        if (spawnOnStart)
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
                        Instantiate(possiblePrefabs[i], transform.position, Quaternion.identity, chunk.transform);
                        break;
                    }
                }
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
