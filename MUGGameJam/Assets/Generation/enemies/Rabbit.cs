using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : WalkingEnemy
{
    public GameObject laserPrefab;
    public Transform laserOut;
    public float laserSpeed;
    void Start()
    {
        base.Start();
    }

    void ShootLaser(Vector3 dir)
    {
        Quaternion rot = Quaternion.LookRotation(dir, Vector3.forward);

        
        ProjectTile las = Instantiate(laserPrefab, laserOut.position, Quaternion.Euler(0, 0, rot.eulerAngles.z)).GetComponent<ProjectTile>();
        las.speed = dir * laserSpeed;
        Debug.Log("shooting");
    }

    void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.W))
            ShootLaser(new Vector3(1, 0, 0));
    }
}
