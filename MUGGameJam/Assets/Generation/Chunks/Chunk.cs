using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{


    GameObject prefab;
    public AnimationCurve chanceOverTime;

    public Transform leftEnd;
    public Transform rightEnd;

    public SelectorSpawner[] groundSpawners;
    public SelectorSpawner[] throwableSpawners;

    public Chunk leftNeighbor, rightNeighbor;
    public float width;
    public Transform player;

    public int minGrounds;
    public int minThrowables;
    public int minEnemies;

    public int groundsSpawned;
    public int throwableSpawned;

    void Start()
    {
        for(int i=0;i<groundSpawners.Length;i++)
        {
            bool spawned = groundSpawners[i].Spawn(minGrounds - groundsSpawned, groundSpawners.Length - i - 1);
            if (spawned)
                groundsSpawned += 1;
        }

        for (int i = 0; i < throwableSpawners.Length; i++)
        {
            bool spawned = throwableSpawners[i].Spawn(minThrowables - throwableSpawned, throwableSpawners.Length - i - 1);
            if (spawned)
                throwableSpawned += 1;
        }
    }

    public bool CheckForPlayer()
    {
        return (transform.position.x - player.position.x) < (width / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
