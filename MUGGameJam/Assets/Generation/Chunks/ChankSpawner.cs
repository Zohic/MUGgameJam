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
    public HandControl hand;

    public Chunk leftChunk, rightChunk;

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

    void ClearChunks(Chunk begin, bool dir, int num)
    {

        List<Chunk> toDelete = new List<Chunk>();
        Chunk cur = begin;

        if (cur == null)
            return;

        for(int i=0;i<num;i++)
        {
            if(dir)
            {
                if(cur.leftNeighbor!=null)
                {
                    toDelete.Add(cur.leftNeighbor);
                    cur = cur.leftNeighbor;
                }
                
            }else
            {
                if (cur.rightNeighbor != null)
                {
                    toDelete.Add(cur.rightNeighbor);
                    cur = cur.rightNeighbor;
                }
            }
        }

        if (dir)
            begin.leftNeighbor = null;
        else
            begin.rightNeighbor = null;

        for (int i = 0; i < toDelete.Count; i++)
        {
            Destroy(toDelete[i].gameObject);
        }

    }

    public Chunk FindChunk(Vector3 pos)
    {
        Chunk playerChunk = beginChunk;
        bool found = true;
        while (!playerChunk.CheckFor(pos))
        {
            if (playerChunk.leftNeighbor != null)
                playerChunk = playerChunk.leftNeighbor;
            else
            {
                found = false;
                break;
            }
        }
        if (!found)
        {
            found = true;
            while (!playerChunk.CheckFor(pos))
            {
                if (playerChunk.rightNeighbor != null)
                    playerChunk = playerChunk.rightNeighbor;
                else
                {
                    found = false;
                    break;
                }
            }
        }

        if (found)
        {
            //Debug.Log(playerChunk.transform.position);
            return playerChunk;
            
        }
        else
        {
            return null;
        }
    }

    public void ClearForSpikes()
    {
        Chunk cur = FindChunk(player.transform.position);
        if(hand.Dir)
        {
            if(cur.leftNeighbor!=null)
                ClearChunks(cur.leftNeighbor, hand.Dir, 10);
            else
                ClearChunks(cur, hand.Dir, 10);
        }
        else
        {
            if (cur.rightNeighbor != null)
                ClearChunks(cur.rightNeighbor, hand.Dir, 10);
            else
                ClearChunks(cur, hand.Dir, 10);
        }
        
        beginChunk = cur;
    }


    public Chunk GetMostLeft()
    {
        Chunk a = beginChunk;
        while (a.leftNeighbor != null)
            a = a.leftNeighbor;
        return a;
    }

    public Chunk GetMostRight()
    {
        Chunk a = beginChunk;
        while (a.rightNeighbor != null)
            a = a.rightNeighbor;
        return a;
    }

    public void AddRandomChunks(Chunk begin, bool dir, int num)
    {
        Chunk prevOne = begin;
        for (int i = 0; i < num; i++)
        {
            prevOne = CreateRandomChunk(prevOne, dir);
        }

        leftChunk = GetMostLeft();
        rightChunk = GetMostRight();

    }

    public void SpawnAfterSpikes()
    {
        Chunk spikeChunk;
        if (hand.Dir)
        {
            spikeChunk = FindChunk(hand.spikes[0].transform.position);
            if(spikeChunk.leftNeighbor!=null)
            {
                ClearChunks(spikeChunk.leftNeighbor, hand.Dir, 10);
                AddRandomChunks(spikeChunk.leftNeighbor, hand.Dir, 4);
            }
            else
            {
                ClearChunks(spikeChunk, hand.Dir, 10);
                AddRandomChunks(spikeChunk, hand.Dir, 4);
            }
            
        }
        else
        {
            spikeChunk = FindChunk(hand.spikes[1].transform.position);
            if (spikeChunk.rightNeighbor != null)
            {
                ClearChunks(spikeChunk.rightNeighbor, hand.Dir, 10);
                AddRandomChunks(spikeChunk.rightNeighbor, hand.Dir, 4);

            }
            else
            {
                ClearChunks(spikeChunk, hand.Dir, 10);
                AddRandomChunks(spikeChunk, hand.Dir, 4);
            }
                
        }
       
        
    }

    void Start()
    {
        AddRandomChunks(beginChunk, true, 4);
        AddRandomChunks(beginChunk, false, 4);
    }

    
    void Update()
    {
        
    }

}
