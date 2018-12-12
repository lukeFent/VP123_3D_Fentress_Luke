using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public float destroyTime = 1; 

	// Use this for initialization
	void Start () {
        StartCoroutine("DestroyObject");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
