using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIcontol : MonoBehaviour {

    public Animator AIcontroller;
    public NavMeshAgent navMeshAgent;
    public GameObject alert;
    public Weapon enemyWeapon;
    public GameObject smoke;
    public Material enemyMaterial;

    //audio
    public AudioSource audioSrc;
    public AudioClip dieSound;


    //detection

    public GameObject player;
    public float detectionRange = 20f;
    public float visionAngle = 100f;
    public float looking = 5;
    public float shootingRange = 2f;
    public bool isAttacking;
    public bool isApproaching; 

    //health

    public float maxHealth = 100;
    private float currentHealth;
    public GameObject healthBar;
    public float damage = 10;


    //patrol
    wayPointConnect currentWayPoint;
    wayPointConnect previousWayPoint;
    public bool canWait;
    public float waitTime = 3f;
    public bool isTravelling;
    public bool isWaiting;
    public float waitTimer;
    public int wayPointsVisited;
    public bool patroling; 

   
   


    // Use this for initialization
    void Start () {

        if (!GetComponent<AudioSource>())
        {
            Debug.Log("No Audio Source");
            gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSrc = GetComponent<AudioSource>();
        }



        currentHealth = maxHealth;
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        //finding waypoints

        if (navMeshAgent == null)
        {
            Debug.LogError("The nav mesh isn't attached to" + gameObject.name);
        }
        else
        {
            if (currentWayPoint == null)
            {
                GameObject[] allWayPoints = GameObject.FindGameObjectsWithTag("wayPointTag");

                if (allWayPoints.Length > 0)
                {
                    while (currentWayPoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWayPoints.Length);
                        wayPointConnect startingWayPoint = allWayPoints[random].GetComponent<wayPointConnect>();
                        if (startingWayPoint != null)
                        {
                            currentWayPoint = startingWayPoint;
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Failed to find the wayPoints");
            }
        }

        //SetDesination();
    }

    // Update is called once per frame
    void Update ()
    {
        //SetDesination();
            //health
            healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 0.2f, 0.2f);


            //detection

            Vector3 toPlayer = player.transform.position - transform.position;
            bool detected = toPlayer.magnitude < detectionRange && Vector3.Angle(toPlayer, transform.forward) < visionAngle;


        if (detected)
        {
            AIcontroller.SetBool("Detected", detected);

            //attacking
            isApproaching = true;
            isAttacking = toPlayer.magnitude < shootingRange && Vector3.Angle(toPlayer, transform.forward) < visionAngle;
          
            

            if (isAttacking)
            {
                AIcontroller.SetBool("Attacking", true);
                isApproaching = false; 
            }


            else
            {
                //AIcontroller.SetBool("Detected", false);
                Debug.Log("Not Attacking");
                AIcontroller.SetBool("Attacking", false);
                patroling = true; 
            }

            detected = false; 

        }
         
        else
        {

             AIcontroller.SetBool("Detected", false);
            isApproaching = false;



        }



       


    }


    public void SetDesination()
    {
        if(patroling)
        {
            if (wayPointsVisited > 0)
            {
                wayPointConnect newWayPoint = currentWayPoint.NextWayPoint(previousWayPoint);
                previousWayPoint = currentWayPoint;
                currentWayPoint = newWayPoint;
            }

            Vector3 targetVector = currentWayPoint.transform.position;
            navMeshAgent.SetDestination(targetVector);
            isTravelling = true;

        }

        else if (isApproaching)
        {
            transform.LookAt(player.transform);
            navMeshAgent.isStopped = true;
            navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.isStopped = false;

        }

        else if (isAttacking)
        {
            navMeshAgent.isStopped = true;
        }

        else
        {
            Debug.Log("NOT SURE WHERE I'M GOING");
        }
    }


    public void damageEnemy(Vector3 damage)
    {
        Debug.Log(currentHealth);
        currentHealth -= damage.magnitude;
        if (currentHealth <= 0)
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
       Instantiate(smoke, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        audioSrc.PlayOneShot(dieSound);

    }

   
}
