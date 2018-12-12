using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float lerpSpeed = 10f;
    public float offsetZ = 5f;
    public float offsetY = 5f;
    public float offsetX = 5f;

	// Use this for initialization
	void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {



        Vector3 newPosition = player.transform.position;
        newPosition += offsetX * player.transform.right;
        newPosition += offsetY * player.transform.up;
        newPosition += offsetZ * player.transform.forward;
        transform.position = Vector3.Lerp(transform.position, newPosition, lerpSpeed * Time.deltaTime);

        //transform.position = player.transform.position - offsetZ * player.forward;

        // transform.position = 
        transform.LookAt(player.transform);	
	}
}
