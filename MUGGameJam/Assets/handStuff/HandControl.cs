using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    public Animator handAnim, fingersAnim, clockAnim;

    public float timeToNext;
    public float workingTimer;
    public bool working;
    public float timer;

    public ScrollControl scroller;

    public float scrollSpeed;
    public bool Dir;

    public GameObject[] spikes;
    public ChankSpawner spawner;



    void Start()
    {
        
    }

    public void CloseFingers()
    {
        fingersAnim.SetTrigger("grab");
        StartCoroutine("StartRotating");
    }

    void SpikeAppear()
    {
        if (Dir)
        {
            spikes[0].SetActive(true);
            spikes[0].GetComponent<Animator>().SetTrigger("come");
        }
        else
        {
            spikes[1].SetActive(true);
            spikes[1].GetComponent<Animator>().SetTrigger("come");
        }
    }

    public void SpikeDisappear()
    {
        if (Dir)
        {
            spikes[0].SetActive(false);
           
        }
        else
        {
            spikes[1].SetActive(false);
        }
        Dir = !Dir;
    }

   

    IEnumerator StartRotating()
    {
        yield return new WaitForSeconds(1);
        handAnim.SetTrigger("rotate");
        scroller.handWorking = true;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=timeToNext && !working)
        {
            working = true;
            handAnim.SetTrigger("start");
            handAnim.SetBool("dir", Dir);
            timer = 0;
            scroller.canScroll = false;
            SpikeAppear();
            spawner.ClearForSpikes();
            clockAnim.SetBool("go", false);
        }
        if(working)
        {
            if(timer>2.5)
                scroller.Translate(Dir, scrollSpeed);
            if(timer>= workingTimer)
            {
                working = false;
                scroller.canScroll = true;
                handAnim.SetTrigger("stop");
                timer = 0;
                if (Dir)
                {
                    spikes[0].GetComponent<Animator>().SetTrigger("stop");
                }
                else
                {
                    spikes[1].GetComponent<Animator>().SetTrigger("stop");
                }
                spawner.SpawnAfterSpikes();
                clockAnim.SetBool("go", true);
                clockAnim.speed = 1.0f / timeToNext;
            }

        }else
            scroller.handWorking = false;
    }
}
