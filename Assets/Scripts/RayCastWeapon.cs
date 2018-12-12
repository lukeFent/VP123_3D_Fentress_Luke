using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWeapon : Weapon {

    public float range = 100;
    public GameObject hitEffect;
    public GameObject bulletTrail;
    public override void LaunchProjectile()
    {
        base.LaunchProjectile();
        RaycastHit hit;
        GameObject trail = Instantiate(bulletTrail, bulletSpawn.position, Quaternion.identity);
        trail.transform.rotation = bulletSpawn.rotation;

        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, range))
        {
            Instantiate(hitEffect, hit.point, Quaternion.identity);
            float distance = Vector3.Distance(bulletSpawn.position, hit.point);
            trail.transform.localScale = new Vector3(1, 1, distance);

            /*
            if (hit.collider.gameObject.GetComponent<EnemyControl>())

            {
                if(hit.collider.gameObject.GetComponent<HitBox>())
                 
                {
                Debug.Log("hit");
                hit.rigidbody.gameObject.GetComponent<EnemyControl>().KillEnemy();
                    hit.collider.gameObject.GetComponent<HitBox>().OnHit.Invoke(damage);



                    //this.gameObject.GetComponent<EnemyControl>().KillEnemy();
                    //Destroy(gameObject);
                }



            */
          
                if (hit.collider.gameObject.GetComponent<HitBox>())

                {
                    Debug.Log("hit");
                    //hit.rigidbody.gameObject.GetComponent<EnemyControl>().KillEnemy();
                    hit.collider.gameObject.GetComponent<HitBox>().OnHit.Invoke(damage * bulletSpawn.forward);



                    //this.gameObject.GetComponent<EnemyControl>().KillEnemy();
                    //Destroy(gameObject);
                }


          

            else
            {
                Debug.Log("not hit");
            }

        }

        else
        {
            trail.transform.localScale = new Vector3(1, 1, range);
        }
    }

}
