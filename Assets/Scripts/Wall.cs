using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public List<Rigidbody> wallPieces = new List<Rigidbody>();

    public void Break(Vector3 damage)
    {
        gameObject.SetActive(false);
        foreach(Rigidbody rb in wallPieces)
        {
            rb.gameObject.transform.parent = null; 
            rb.gameObject.SetActive(true);
            rb.isKinematic = false;
            rb.AddForce((damage.normalized + Random.Range(-1.0f,1.0f) * Vector3.Cross(damage.normalized, Vector3.up)) * 100, ForceMode.Impulse);

        }
    }

}
