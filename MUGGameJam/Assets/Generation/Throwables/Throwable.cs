using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    bool grabbed;
    PlayerControl owner;
    public float holdRotation;
    public Transform stickPosition;
    
    void Start()
    {
        
    }

    public void GetGrabbed(PlayerControl p)
    {
        owner = p;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;
        
    }

    public void GetThrown(Vector2 throwVelocity)
    {
        owner = null;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        GetComponent<BoxCollider2D>().enabled = true;
        rb.velocity = throwVelocity;

    }

    // Update is called once per frame
    void Update()
    {
        if(owner!=null)
        {
            transform.position = Vector3.Lerp(transform.position, owner.holder.position, Time.deltaTime*5);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, holdRotation*owner.orientation), Time.deltaTime*1.5f);
        }
    }
}
