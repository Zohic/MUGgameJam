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

    public GameObject hitParticle;

    public bool thrown;
    public LayerMask groundLayer, enemyLayer;

    public GameObject hitSound;
    public float gravityScale;

    void Start()
    {
    }

    public void GetGrabbed(PlayerControl p)
    {
        owner = p;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        //rb.isKinematic = true;
        //GetComponent<BoxCollider2D>().enabled = false;
        gameObject.layer = 11;
        rb.gravityScale = 0;
    }

    public void GetThrown(Vector2 throwVelocity)
    {
        owner = null;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.isKinematic = false;
        //GetComponent<BoxCollider2D>().enabled = true;
        rb.gravityScale = gravityScale;
        rb.velocity = throwVelocity;
        GetComponentInChildren<Animator>().SetBool("thrown", true);
        thrown = true;
        gameObject.layer = 8;
    }

    public void Release()
    {
        owner = null;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.isKinematic = false;
        //GetComponent<BoxCollider2D>().enabled = true;
        rb.gravityScale = gravityScale;
        rb.velocity = Vector2.zero;
        GetComponentInChildren<Animator>().SetBool("thrown", false);
        thrown = false;
        gameObject.layer = 8;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(thrown)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                //Debug.Log(rb.velocity.magnitude);
                if (rb.velocity.magnitude > enemy.killObjectSpeed)
                {
                    Destroy(Instantiate(hitSound, transform.position, Quaternion.identity), 1);
                    rb.velocity = Vector2.zero;
                    Destroy(Instantiate(hitParticle, transform.position + new Vector3(0, 0, -5), Quaternion.identity), 1);
                    Destroy(gameObject);
                    Destroy(enemy.gameObject);
                }
            }
            
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
            {
                Debug.Log(collision.gameObject.layer);
                Release();
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
