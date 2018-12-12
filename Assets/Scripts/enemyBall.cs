using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBall : MonoBehaviour {


    public float initialThrust = 10;
    public GameObject blast;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GiveInitialVelocity()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward + new Vector3(1, initialThrust), ForceMode.Impulse);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<playerControls>())
        {
            Instantiate(blast, transform.position, Quaternion.identity);
            Debug.Log("hit");
            collision.gameObject.GetComponent<playerControls>().KillPlayer();

            if (collision.gameObject.GetComponent<playerControls>().isDead == true)
            {
                //state machinie script
            }
        }
        else
        {
            Debug.Log("Not hit");
        }
        Destroy(this.gameObject);
    }
}
