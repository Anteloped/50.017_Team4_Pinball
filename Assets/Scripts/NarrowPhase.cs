using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

// narrow phase of collision detection
public class NarrowPhase : MonoBehaviour
{
    GameObject ball;
    Ball ballScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // checks collision between ball and the specified collidingObject, then updates the ball accordingly
    public void collide(GameObject collidingObject)
    {
        Debug.Log("collision function called");

        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        Ball ballScript = ball.GetComponent<Ball>();

        Vector3 ballPosition = ball.transform.position;
        Vector3 ballVelocity = ballScript.getVelocity();

        Vector3 collidingObjectPosition = collidingObject.transform.position;

        Parameters parameters = GameObject.Find("PlayArea").GetComponent<Parameters>();

        float tolerance = 0.05f; // if the distance between objects falls below this tolerance, consider it as a collision

        // check collision with walls
        switch (collidingObject.name)
        {
            // if the ball is colliding with TopBoundary, the ball's velocity is reversed in the z direction
            case "TopBoundary":
                // if the ball is travelling towards the collidingObject
                if (ballVelocity[2] > 0)
                {
                    // check the z-coordinate to see if the collision occurred
                    if (ballPosition[2] > collidingObjectPosition[2] - tolerance)
                    {
                        ballVelocity[2] = -ballVelocity[2];
                        ballScript.setVelocity(ballVelocity);
                    }
                }
                break;

            // if the ball is colliding with LeftBound, the ball's velocity is reversed in the x direction
            case "LeftBound":
                // if the ball is travelling towards the collidingObject
                if (ballVelocity[0] < 0)
                {
                    // check the x-coordinate to see if the collision occurred
                    if (ballPosition[0] < collidingObjectPosition[0] + tolerance)
                    {
                        ballVelocity[0] = -ballVelocity[0];
                        ballScript.setVelocity(ballVelocity);
                    }
                }
                break;

            // if the ball is colliding with RightBound, the ball's velocity is reversed in the x direction
            case "RightBound":
                // if the ball is travelling towards the collidingObject
                if (ballVelocity[0] > 0)
                {
                    // check the x-coordinate to see if the collision occurred
                    if (ballPosition[0] > collidingObjectPosition[0] - tolerance)
                    {
                        ballVelocity[0] = -ballVelocity[0];
                        ballScript.setVelocity(ballVelocity);
                    }
                }
                break;

            // For the following cases: If the ball is colliding with the specified GameObject, the ball's velocity is
            // reflected according to the surface normal
            // reflected velocity, r: r = v - 2 * Vector3.Dot(v, n.normalized) * n.normalized
            //   where v is the original velocity, and
            //   n is the normal of the surface that the ball is colliding with.
            case "TopCornerboundRight":
                break;

            case "TopCornerboundLeft":
                break;

            case "BotLeftBoundary":
                break;

            case "BotRightBoundary":
                break;

            case "Deflector":
                break;
        }

        // check collision with bumpers
        if (collidingObject.tag == "Bumper")
        {
            Vector3 bumperDirection = collidingObjectPosition - ballPosition;

            // if the ball is travelling towards the collidingObject
            if (Vector3.Dot(ballVelocity, bumperDirection) > 0)
            {
                // check the distance to see if the collision occurred
                float bumperDistance = bumperDirection.magnitude;
                float bumperRadius = collidingObject.transform.localScale[0];

                if (bumperDistance < bumperRadius + tolerance)
                {
                    // assuming the bumper power (SHOULD INSTEAD GET THE VALUE FROM PLAYER-SELECTED GLOBAL PARAMETER)
                    float bumperPower = parameters.getBumperPower(); // affects the velocity increase when the ball hits the bumper
                    Vector3 bumperNormal = -bumperDirection.normalized;
                    ballVelocity = Vector3.Reflect(ballVelocity, bumperNormal);
                    ballVelocity += (bumperPower * bumperNormal);
                    ballScript.setVelocity(ballVelocity);
                }
            }
        }

        // (needed ?) check collision with flippers
    }
}