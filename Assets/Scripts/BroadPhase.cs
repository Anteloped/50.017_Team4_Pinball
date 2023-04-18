using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadPhase : MonoBehaviour
{
    List<GameObject> topLeftObjects;
    List<GameObject> topRightObjects;
    List<GameObject> bottomLeftObjects;
    List<GameObject> bottomRightObjects;

    NarrowPhase narrowPhaseCollision;

    // Start is called before the first frame update
    void Start()
    {
        topLeftObjects = new List<GameObject>();
        topRightObjects = new List<GameObject>();
        bottomLeftObjects = new List<GameObject>();
        bottomRightObjects = new List<GameObject>();

        topLeftObjects.Add(GameObject.Find("TopBoundary"));
        topLeftObjects.Add(GameObject.Find("LeftBound"));
        topLeftObjects.Add(GameObject.Find("Bumper7"));
        topLeftObjects.Add(GameObject.Find("Bumper5"));
        topLeftObjects.Add(GameObject.Find("Bumper4"));

        topRightObjects.Add(GameObject.Find("TopBoundary"));
        topRightObjects.Add(GameObject.Find("RightBound"));
        topRightObjects.Add(GameObject.Find("Bumper6"));
        topRightObjects.Add(GameObject.Find("Bumper4"));
        topRightObjects.Add(GameObject.Find("Bumper3"));

        bottomLeftObjects.Add(GameObject.Find("LeftBound"));
        bottomLeftObjects.Add(GameObject.Find("Bumper2"));

        bottomRightObjects.Add(GameObject.Find("RightBound"));
        bottomRightObjects.Add(GameObject.Find("Bumper1"));

        narrowPhaseCollision = GameObject.Find("PlayArea").GetComponent<NarrowPhase>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void collideWithNearby()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        Ball ballScript = ball.GetComponent<Ball>();

        Vector3 ballPosition = ball.transform.position;


        // if ball is in top left bounding box
        if (ballPosition[0] <= 0 && ballPosition[2] > 0)
        {
            foreach (GameObject collidingObject in topLeftObjects)
            {
                narrowPhaseCollision.collide(collidingObject);
            }
        }


        // if ball is in top right bounding box
        else if (ballPosition[0] > 0 && ballPosition[2] > 0)
        {
            foreach (GameObject collidingObject in topRightObjects)
            {
                narrowPhaseCollision.collide(collidingObject);
            }
        }


        // if ball is in bottom left bounding box
        else if (ballPosition[0] <= 0 && ballPosition[2] <= 0)
        {
            foreach (GameObject collidingObject in bottomLeftObjects)
            {
                narrowPhaseCollision.collide(collidingObject);
            }
        }


        // if ball is in bottom right bounding box
        else
        {
            foreach (GameObject collidingObject in bottomRightObjects)
            {
                narrowPhaseCollision.collide(collidingObject);
            }
        }



    }
}
