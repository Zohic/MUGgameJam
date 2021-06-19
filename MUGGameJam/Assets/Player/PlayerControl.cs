using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    //refs
    public Animator animator;
    Rigidbody2D rigid;
    public Transform groundChecker;
    public LayerMask groundMask;

    //gameplay vars
    public float spd;
    public float jumpSpeed;
    public int orientation;
    public bool grounded = false;
    public float groundCheckerRadius;
    public bool touchingSomething;
    public SpriteRenderer spriteRenderer;

    //animation
    public bool midair = false;
    public bool walking = false;




    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {
        walking = false;

        if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector2(-spd, rigid.velocity.y);
            //transform.Translate(new Vector3( * Time.deltaTime, 0, 0));
            if (!midair)
            {
                animator.SetBool("walking", true);
            }

            orientation = 1;
            spriteRenderer.flipX = true;
            walking = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector2(spd, rigid.velocity.y);
            //transform.Translate(new Vector3(spd * Time.deltaTime, 0, 0));
            if (!midair)
            {
                animator.SetBool("walking", true);
            }
            orientation = 0;
            spriteRenderer.flipX = false;
            walking = true;
        }
        else
            animator.SetBool("walking", false);

        touchingSomething = rigid.IsTouchingLayers(groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            animator.SetBool("walking", false);
            //animator.SetBool("landed", false);
            animator.SetBool("falling", false);
            animator.SetTrigger("jumped");
            grounded = false;
            touchingSomething = false;
            midair = true;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
            transform.Translate(0, 10, 0);
        }

        if (touchingSomething)
        {
            
            Collider2D[] grounds = Physics2D.OverlapCircleAll(groundChecker.position, groundCheckerRadius, groundMask);
            if (grounds.Length > 0)
            {
                midair = false;
                grounded = true;
                animator.SetBool("grounded", true);
            }
        }
        else
        {
            animator.SetBool("grounded", false);
            grounded = false;
            midair = true;
        }

        if(midair)
        {
            if (rigid.velocity.y < 200)
                animator.SetBool("falling", true);
        }else
        {
            animator.SetBool("falling", false);
        }
        /*else
        {
            Collider2D[] grounds = Physics2D.OverlapCapsuleAll(groundChecker.position, new Vector2(1, groundCheckerRadius * 2), CapsuleDirection2D.Horizontal, 0, groundMask);
            if (grounds.Length == 0)
            {
                animator.SetBool("walking", false);
                animator.SetBool("landed", false);
                animator.SetTrigger("midair");
                midair = true;
                grounded = false;
            }
        }*/

    }
    private void LateUpdate()
    {
        if (Physics2D.OverlapCircleAll(groundChecker.position, groundCheckerRadius, groundMask).Length>0 && midair)
        {
            if (rigid.velocity.y < 0)
            {
                animator.SetBool("grounded", true);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerRadius);
    }
}
