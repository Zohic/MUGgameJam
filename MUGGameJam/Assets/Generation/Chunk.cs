using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{


    GameObject prefab;
    public AnimationCurve chanceOverTime;

    public Transform leftEnd;
    public Transform rightEnd;

    public Transform[] groundPlaces;
    public List<GameObject> groundPrefabs;

    public Chunk leftNeighbor, rightNeighbor;
    public float width;
    public Transform player;

    void Start()
    {
        
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
