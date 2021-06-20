using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : WalkingEnemy
{
    public GameObject laserPrefab;
    public Transform laserOut;
    public Transform lookCheck;
    public float laserSpeed;
    public LayerMask groundMask;
    public bool attacking;
    public float timer;
    public float attackspeed;


    void Start()
    {
        base.Start();
    }

    void ShootLaser(Vector3 dir)
    {
        
        ProjectTile las = Instantiate(laserPrefab, laserOut.position, Quaternion.identity).GetComponent<ProjectTile>();
        //Vector3 xDirection = (lookTarget - transform.position).normalized;

        Vector3 yDirection = Quaternion.Euler(0, 0, 90) * dir;

        las.transform.rotation = Quaternion.LookRotation(Vector3.forward, yDirection);
        las.speed = dir * laserSpeed;
    }

    void ShootPlayer()
    {
        Vector3 dif = player.transform.position - laserOut.position;
        ShootLaser(dif.normalized);
    }

    void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.W))
            ShootLaser(new Vector3(1, 0, 0));

        float dist = Mathf.Abs(player.transform.position.x - transform.position.x);

        if (dist < 2800)
        {

            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(groundMask);
            Physics2D.Linecast(lookCheck.position, player.transform.position, filter, hits);

            if (hits.Count < 1)
            {
                Debug.Log("SEE YOU");
                walking = false;
                animator.SetBool("walking", false);
                if ((player.transform.position - transform.position).x > 0)
                    orientation = 1;
                else
                    orientation = -1;

                rigid.velocity = new Vector2(-rigid.velocity.x, rigid.velocity.y);
                transform.localScale = new Vector2(orientation, 1);

                timer += Time.deltaTime;
                if (timer >= attackspeed)
                {
                    timer = 0;
                    ShootPlayer();
                }
            }
            else
            {
                timer = 0;
                animator.SetBool("walking", true);
                walking = true;
            }
        }else
        {
            timer = 0;
            animator.SetBool("walking", true);
            walking = true;
        }
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(laserOut.position, player.transform.position);

    }

}
