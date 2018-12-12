using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBlaster : Weapon {


    public float range = 100;
    public GameObject hitEffect;
    public GameObject ball; 
    public override void LaunchProjectile()
    {
        base.LaunchProjectile();
        GameObject newBall = Instantiate(ball, bulletSpawn.transform.position, Quaternion.identity);
        newBall.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward * 30; 

    }
}
