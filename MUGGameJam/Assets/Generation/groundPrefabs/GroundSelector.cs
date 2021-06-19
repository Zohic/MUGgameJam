using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSelector : MonoBehaviour
{
    public GameObject[] possiblePrefabs;
    [SerializeField]
    float[] chances;
    void Start()
    {
        float rand = Random.Range(0.0f, 1.0f);
        float sum = 0;
        for(int i=0;i<chances.Length;i++)
        {
            sum += chances[i];
            if(rand <= sum)
            {
                if (i < chances.Length - 1)
                    Instantiate(possiblePrefabs[i], transform.position, Quaternion.identity);
                break;
            }
        }
        
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
