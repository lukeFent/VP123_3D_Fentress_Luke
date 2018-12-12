using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalRotation : MonoBehaviour {

    public float rotateSpeed = 1f;
    public float maxiumAngleUp = 15f;
    public float maxiumAngleDown = -20f;

    private float currentAngle = 0f; 
    public Transform player; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rotate(); 
	}

    void Rotate ()
    {

        float deltaRotation = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        currentAngle += deltaRotation;
        currentAngle = Mathf.Clamp(currentAngle, -maxiumAngleDown, maxiumAngleUp);
        float angleInRadians = currentAngle * Mathf.Deg2Rad;
        Vector3 newLookAtDirection = Mathf.Cos(angleInRadians) * player.forward + Mathf.Sin(angleInRadians) *player.up;
        Vector3 newLookAtPoint = transform.position + newLookAtDirection;
        transform.LookAt(newLookAtPoint);

    }
}
