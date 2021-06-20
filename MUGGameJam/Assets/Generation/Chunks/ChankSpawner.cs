using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChankSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] chunkPrefabs;

    [SerializeField]
    GameObject beginChunkObj;
    [SerializeField]
    Chunk beginChunk;

    public PlayerControl player;


    void Attach(Chunk movedOne, Chunk stilOne, bool left)
    {
        if(left)
        {
            movedOne.transform.position = (stilOne.leftEnd.position - movedOne.rightEnd.localPosition);
            stilOne.leftNeighbor = movedOne;
            movedOne.rightNeighbor = stilOne;
        }
        else
        {
            movedOne.transform.position = (stilOne.rightEnd.position - movedOne.leftEnd.localPosition);
            stilOne.rightNeighbor = movedOne;
            movedOne.leftNeighbor = stilOne;
        }
    }

    Chunk CreateRandomChunk(Chunk where, bool left)
    {
        Chunk newChunk = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)]).GetComponent<Chunk>();
        newChunk.player = player;
        Attach(newChunk, where, left);
        return newChunk;

    }

    void Start()
    {
        Chunk prevOne = beginChunk;
        for (int i = 0; i < 3; i++) 
        {
            prevOne = CreateRandomChunk(prevOne, false);
        }

        prevOne = beginChunk;
        for (int i = 0; i < 3; i++)
        {
            prevOne = CreateRandomChunk(prevOne, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
