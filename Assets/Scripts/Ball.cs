using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField] Vector3 endGameCoord;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float tableAngle = 7f; // the angle at which the table slants forward
    [SerializeField] Transform table;

    static float mass = 0.08f;
    static float friction = 0.1f;
    Vector3 vel;
    Vector3 acc = Vector3.zero;
    Vector3 grav;
    bool rolling = false;

    void Start() {
        gravity *= Mathf.Sin(Mathf.Deg2Rad * tableAngle); // use the component of gravity in the direction parallel to the table
        grav = new Vector3(0, 0, gravity);
        vel = new Vector3(0, 0, -1.5f);
    }

    void Update()
    {
        if ((transform.position.z - endGameCoord.z) <= 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void FixedUpdate() {
        // Update grav vector based on the table's tilt angle; would be good if we used vector ops instead of sine
        grav = new Vector3(gravity * Mathf.Sin(-table.rotation.eulerAngles.z * Mathf.Deg2Rad), 0, gravity);


        // Update ball position
        Vector3 pos = transform.position;
        pos += vel * Time.fixedDeltaTime; // + acc * Mathf.Pow(Time.deltaTime, 2) / 2;
        //pos.x = Mathf.Clamp(pos.x, -2.6f, 3.2f);
        pos.y = 0.16f;
        //pos.z = Mathf.Clamp(pos.z, endGameCoord.z - 1, 6.05f);
        transform.position = pos;

        // Update ball velocity
        vel += acc * Time.fixedDeltaTime;

        // Update ball acceleration
        if (!rolling) {
            acc += grav * Time.fixedDeltaTime;
        }
        acc -= acc * friction * Time.fixedDeltaTime;
    }

    public void ApplyForce(Vector3 force) {
        acc += force / mass;
        vel += acc;
    }

    void OnCollisionEnter(Collision collision) {
        GameObject obj = collision.gameObject;
        Vector3 normal;

        for (int i = 0; i < collision.contactCount; i++) {
            normal = collision.GetContact(i).normal;

            // Ball bounces off if it hits a bumper
            if (obj.CompareTag("Bumper")) {
                vel = Vector3.Reflect(vel, normal);
            }
            // Otherwise, movement in the direction of the obstacle is cancelled out; "equal and opposite reaction"
            else if (!obj.CompareTag("Untagged")) {
                vel -= Vector3.Dot(vel, normal) * normal;
                acc -= Vector3.Dot(acc, normal) * normal;
            }
        }
    }

    // For the scenarios where the obstacle is below the ball, so that it doesn't fall through
    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Plunger")) {
            acc = Vector3.zero;
            //vel = Vector3.zero; // may cause plunger to be "sticky", where it charges up but doesn't launch
        }
        // Roll along the wall
        else if (collision.gameObject.CompareTag("Wall")) {
            Vector3 normal;
            
            for (int i = 0; i < collision.contactCount; i++) {
                normal = collision.GetContact(i).normal;
                acc = Vector3.Cross(normal, Vector3.Cross(grav, normal));
            }

            rolling = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        rolling = false;
    }
}
