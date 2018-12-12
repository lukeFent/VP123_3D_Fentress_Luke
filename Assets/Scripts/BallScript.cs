using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public float initialThrust = 10;
    public GameObject blast;
    public int damage = 5;
    private Transform myTrans;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        myTrans = this.transform;

           
	}

    public void GiveInitialVelocity()
    {
       GetComponent<Rigidbody>().AddForce(transform.forward + new Vector3(1, initialThrust), ForceMode.Impulse);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HitBox>())
        {
            Instantiate(blast, transform.position, Quaternion.identity);
            Debug.Log("hit");
            //collision.gameObject.GetComponent<EnemyControl>().KillEnemy();
            collision.gameObject.GetComponent<HitBox>().OnHit.Invoke(damage * myTrans.forward);

        }
        else
        {
            Debug.Log("Not hit"); 
        }
        //Destroy(this.gameObject);
    }
}
