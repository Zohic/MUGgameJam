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
    public SelectorSpawner[] enemiesSpawners;

    public Chunk leftNeighbor, rightNeighbor;
    public float width;
    public PlayerControl player;

    public int minGrounds;
    public int minThrowables;
    public int minEnemies;

    public int groundsSpawned;
    public int throwableSpawned;
    public int enemiesSpawned;

    void Start()
    {
        GameObject go;
        for (int i=0;i<groundSpawners.Length;i++)
        {
            groundSpawners[i].chunk = this;
            bool spawned = groundSpawners[i].Spawn(minGrounds - groundsSpawned, groundSpawners.Length - i - 1, out go);
            if (spawned)
                groundsSpawned += 1;
        }

        for (int i = 0; i < throwableSpawners.Length; i++)
        {
            throwableSpawners[i].chunk = this;
            bool spawned = throwableSpawners[i].Spawn(minThrowables - throwableSpawned, throwableSpawners.Length - i - 1, out go);
            if (spawned)
                throwableSpawned += 1;
        }

        for (int i = 0; i < enemiesSpawners.Length; i++)
        {
            enemiesSpawners[i].chunk = this;
            bool spawned = enemiesSpawners[i].Spawn(minEnemies - enemiesSpawned, enemiesSpawners.Length - i - 1, out go);
            if (spawned)
            {
                go.GetComponent<Enemy>().player = player;
                enemiesSpawned += 1;
            }
                
        }
    }

    public bool CheckFor(Vector3 pos)
    {
        return Mathf.Abs((transform.position.x - pos.x)) < (width / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
