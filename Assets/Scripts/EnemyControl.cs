using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {
    //public Transform target;
    public GameObject smoke;
   // public Transform wayPoints;
    public bool canWait;
    public float waitTime = 3f;
    //float _switchChance = 0.2f;
    wayPointConnect currentWayPoint;
    wayPointConnect previousWayPoint;
    NavMeshAgent navMeshAgent;
    bool isTravelling;
    bool isWaiting;
    float waitTimer;
    int wayPointsVisited;
    public float maxHealth = 100;
    private float currentHealth;
    public GameObject healthBar;
    public float damage = 10;
    public bool patroling; 



	// Use this for initialization
	public void Start () {
        currentHealth = maxHealth; 
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("The nav mesh isn't attached to" + gameObject.name);
        }
        else
        {
            if (currentWayPoint == null)
            {
                GameObject[] allWayPoints = GameObject.FindGameObjectsWithTag("wayPointTag");
                                                     
                if(allWayPoints.Length > 0)
                {
                    while(currentWayPoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWayPoints.Length);
                        wayPointConnect startingWayPoint = allWayPoints[random].GetComponent<wayPointConnect>();
                        if(startingWayPoint != null)
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

    public void Update()
    {

        if(isTravelling && navMeshAgent.remainingDistance <= 2.0f)
        {
            isTravelling = false;
            wayPointsVisited++;

            if (canWait)
            {
                isWaiting = true;
                waitTimer = 0f;
            }
            else
            {

                //patroling = true; 
                SetDesination();
            }

        }

        if(isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                SetDesination();
            }
        }
    }

    private void SetDesination()
    {
        if (patroling)
        {
        if(wayPointsVisited > 0)
        {
            wayPointConnect newWayPoint = currentWayPoint.NextWayPoint(previousWayPoint);
            previousWayPoint = currentWayPoint;
            currentWayPoint = newWayPoint;
        }

        Vector3 targetVector = currentWayPoint.transform.position;
        navMeshAgent.SetDestination(targetVector);
        isTravelling = true;
        }
    }
	
	// Update is called once per frame
	//void Update () {
        //GetComponent<NavMeshAgent>().SetDestination(wayPoints[].position);
		
//	}

    public void damageEnemy(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        Instantiate(smoke, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }
}
