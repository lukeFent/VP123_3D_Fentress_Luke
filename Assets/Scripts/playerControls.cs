using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour {
    public float forwardSpeed =100;
    public bool isDead;
    public float sidewaysSpeed = 8;
    private Rigidbody body;
    public float jumpSpeed = 10;
   public float rotateSpeed = 50;
    // Use this for initialization
    public Weapon currentWeapon;
    public GameObject smoke;
    public List<Weapon> myWeapons = new List<Weapon>();
    public int index = 0;
    public Animator anim;
    private int jumpCount = 0; 




    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        
    }

    void Start ()
    {
        if (!GetComponent<Animator>())
        {
            Debug.Log("No animator");
            gameObject.AddComponent<Animator>();
        }
        else
        {
            anim = GetComponent<Animator>();
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Move(Input.GetAxisRaw("Horizontal"));

        Jump();
        Rotate();
        Fire();
        cycleWeapon();
	}

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Vector3 moveVel = body.velocity;
        //moveVel.x = horizontalInput * forwardSpeed;
        anim.SetFloat("xDirection", Input.GetAxis("Horizontal"));
        anim.SetFloat("yDirection", Input.GetAxis("Vertical"));

        Vector3 horizontal = Input.GetAxis("Horizontal") * sidewaysSpeed * transform.right;
        Vector3 forward = Input.GetAxis("Vertical") * forwardSpeed * transform.forward;
        //transform.position += horizontal + forward; 

        body.AddForce(horizontal + forward);
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && jumpCount < 1)
        {

            body.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            jumpCount++;
            anim.SetBool("jump", true);
        }


    }

    void Rotate()
    {


        float deltaRotation = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
     
        Vector3 newLookAtDirection =  transform.forward + deltaRotation * transform.right;
        Vector3 newLookAtPoint = transform.position + newLookAtDirection;
        transform.LookAt(newLookAtPoint);
    }

    void Fire()
    {
       
            currentWeapon.fireControl(Input.GetButtonDown("Fire1"));
        //GetComponent<AudioClip>().
        
     
    }
    public void KillPlayer()
    {
        Instantiate(smoke, transform.position, Quaternion.identity);
       //Destroy(this.gameObject);
        isDead = true; 

    }

    public void cycleWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            index++;
            if (index >= myWeapons.Count)
            {
                index = 0;
            }
            currentWeapon = myWeapons[index];

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 13)
        {
            jumpCount = 0;
            anim.SetBool("jump", false);
        }
    }
}
