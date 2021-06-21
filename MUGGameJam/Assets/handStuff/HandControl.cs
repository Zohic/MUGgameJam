using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    public Animator handAnim, fingersAnim, clockAnim;

    public float timeToNext = 45;
    public float workingTimer;
    public bool working;
    public float timer;

    public ScrollControl scroller;

    public float scrollSpeed;
    public bool Dir;

    public GameObject[] spikes;
    public ChankSpawner spawner;

    public AudioSource music;
    public AudioClip common;
    public AudioClip rush;

    void Start()
    {
        clockAnim.SetBool("go", true);
        clockAnim.speed = 1.0f / timeToNext;
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
        music.clip = common;
        music.Play();
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
            music.clip = rush;
            music.Play();
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
                timeToNext = Random.Range(45, 60);
                clockAnim.SetBool("go", true);
                clockAnim.speed = 1.0f / timeToNext;
            }

        }else
            scroller.handWorking = false;
    }
}
