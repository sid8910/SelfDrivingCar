using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{   
    public PathCreator pathCreator;
    float distanceTravelled = 0;
    public float speed = 5;
    Vector3 waypoint, prevWaypoint;
    //public float[] waypoint = new float[1000];
    int i = 0;
    float distanceTravelled1;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 waypoint;
        while (true)
        {
            waypoint = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            distanceTravelled += 5;
            print(waypoint);
            if (waypoint == prevWaypoint)
            {
                break;
            }
            prevWaypoint = waypoint;
        }

       
    }



    // Update is called once per frame
    void Update()
    {
        /*distanceTravelled1 += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled1);
        distanceTravelledact = distanceTravelledact + (float)3;
        distanceTravelled = pathCreator.path.GetPointAtDistance(distanceTravelledact);
        //waypoint[i] = distanceTravelled;
        print(distanceTravelled);
        i++;        */

    }
}
