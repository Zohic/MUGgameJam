using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    public Animator handAnim, fingersAnim;
   
    void Start()
    {
        
    }

    public void CloseFingers()
    {
        fingersAnim.SetTrigger("grab");
        StartCoroutine("StartRotating");
    }

    IEnumerator StartRotating()
    {
        yield return new WaitForSeconds(1);
        handAnim.SetTrigger("rotate");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
