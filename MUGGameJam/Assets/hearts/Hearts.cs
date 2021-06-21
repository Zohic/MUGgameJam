using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public GameObject[] heartsObj;
    int lives = 3;
    void Start()
    {
        
    }

    public void LoseLife()
    {
        if(lives>0)
        {
            heartsObj[lives - 1].GetComponent<Animator>().SetTrigger("die");
            Destroy(heartsObj[lives - 1], 0.9f);
            lives -= 1;
        }
       
    }
    
    public void Die()
    {

    }

    void Update()
    {
        
    }
}
