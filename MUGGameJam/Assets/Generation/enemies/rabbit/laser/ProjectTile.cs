using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public Vector3 speed;
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControl plr = collision.gameObject.GetComponent<PlayerControl>();
        if (plr != null)
        {
            plr.lives -= 1;
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer != 9 && collision.gameObject.layer != 12)
        {
            Destroy(gameObject);
        }
        Debug.Log(collision.gameObject.layer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl plr = collision.gameObject.GetComponent<PlayerControl>();
        if (plr != null)
        {
            plr.lives -= 1;
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer != 9 && collision.gameObject.layer != 12)
        {
            Destroy(gameObject);
        }
        Debug.Log(collision.gameObject.layer);
    }

}
