using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    Vector3 distanceTravelled;
    float distanceTravelledact;
    public float speed = 5;
    public float[] waypoint = new float[1000];
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

       
    }



    // Update is called once per frame
    void Update()
    {
        /*      distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                print(distanceTravelled);

        */
        distanceTravelledact = distanceTravelledact + (float)3;
        distanceTravelled = pathCreator.path.GetPointAtDistance(distanceTravelledact);
        //waypoint[i] = distanceTravelled;
        print(distanceTravelled);
        i++;
    }
}
