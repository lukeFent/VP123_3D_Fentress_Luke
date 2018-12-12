using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPointConnect : WayPoints {
   

    [SerializeField]
    protected float connectRadius = 50f;
    List<wayPointConnect> connections; 

	// Use this for initialization
	public void Start () {

        GameObject[] allWayPoints = GameObject.FindGameObjectsWithTag("wayPointTag");
        connections = new List<wayPointConnect>();

        for (int i = 0; i < allWayPoints.Length; i++)
        {
            wayPointConnect nextWayPoint = allWayPoints[i].GetComponent<wayPointConnect>();
        
            if(nextWayPoint != null)
            {

                if(Vector3.Distance(this.transform.position, nextWayPoint.transform.position) <= connectRadius && nextWayPoint != this)
                {
                    connections.Add(nextWayPoint);  
                }

            }
        
        }

	}

    public override void onDrawGizmos()
    {
        base.onDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, connectRadius);
    }


    public wayPointConnect NextWayPoint(wayPointConnect previousWaypoint)
    {
        if(connections.Count == 0)
        {
            Debug.LogError("Insufficent way point count");
            return null;
        }

        else if(connections.Count == 1 && connections.Contains(previousWaypoint))
        {
            return previousWaypoint;
        }
        else
        {
            wayPointConnect nextWayPoint;
            int nextIndex = 0;

            do
            {
                nextIndex = UnityEngine.Random.Range(0, connections.Count);
                nextWayPoint = connections[nextIndex];
            } while (nextWayPoint == previousWaypoint);

                return nextWayPoint;
        }
    }
 
}
