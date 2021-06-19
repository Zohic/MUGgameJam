using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    bool grabbed;
    PlayerControl owner;
    public float holdRotation;
    public Transform stickPosition;
    public float followSpeed;
    void Start()
    {
        
    }

    public void GetGrabbed(PlayerControl p)
    {
        owner = p;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy!=null)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //Debug.Log(rb.velocity.magnitude);
            if (rb.velocity.magnitude>enemy.killObjectSpeed)
            {
                rb.velocity = Vector2.zero;
                Destroy(enemy.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(owner!=null)
        {
            transform.position = Vector3.Lerp(transform.position, owner.holder.position, Time.deltaTime* followSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, holdRotation*owner.orientation), Time.deltaTime*1.5f);
        }
    }
}
