using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    [SerializeField]
    Transform frontChecker;
    [SerializeField]
    float checkerRadius;
    [SerializeField]
    LayerMask groundLayer;
    Rigidbody2D rigid;

    public float speed;

    Animator animator;

    int orientation=1;

    protected void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        rigid.velocity = new Vector2(speed, 0);
    }

    void ChangeOrientation()
    {
        orientation = -orientation;
        rigid.velocity = new Vector2(-rigid.velocity.x, rigid.velocity.y);
        transform.localScale = new Vector2(orientation, 1);
        
    }
    
    protected void Update()
    {
        rigid.velocity = new Vector2(speed*orientation, rigid.velocity.y);
        if (Physics2D.OverlapCircleAll(frontChecker.position, checkerRadius, groundLayer).Length>0)
        {
            ChangeOrientation();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(frontChecker.position, checkerRadius);
       
    }
}
