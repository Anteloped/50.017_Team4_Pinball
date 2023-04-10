using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField] Vector3 endGameCoord;
    float maxFallSpeed = -10f;
    float maxFallAcc = -10f;

    public float gravity = -10f;
    public float tableAngle = 7f; // the angle at which the table slants forward
    public Transform table;
    //[SerializeField] int lives = 3;

    static float mass = 0.08f;
    static float friction = 0.01f;
    static float radius = 0.16f;
    Vector3 vel;
    Vector3 acc;
    Vector3 grav;
    bool rolling = false;
    bool launched = false;
    bool flipped = false;
    Vector3 flipperNormal;
    Vector3 flipperPoint;
    Vector3 startPos;

    void Start() {
        //startPos = transform.position;
        Init();
    }
    
    void Init() {
        //transform.position = startPos;
        //gravity *= Mathf.Sin(Mathf.Deg2Rad * tableAngle); // use the component of gravity in the direction parallel to the table
        grav = new Vector3(0, 0, gravity);
        vel = new Vector3(0, 0, -1.5f);
        acc = Vector3.zero;
    }

    /*
    void Update()
    {
        if ((transform.position.z - endGameCoord.z) <= 0){
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            lives -= 1;
            if (lives > 0) {
                Init();
            }
        }
    }
    */

    void FixedUpdate() {
        // Update grav vector based on the table's tilt angle; would be good if we used vector ops instead of sine
        grav = new Vector3(gravity * Mathf.Sin(-table.rotation.eulerAngles.z * Mathf.Deg2Rad), 0, gravity);

        // Update ball position
        Vector3 pos = transform.position;
        pos += vel * Time.fixedDeltaTime; // + acc * Mathf.Pow(Time.deltaTime, 2) / 2;
        pos.y = radius;
        transform.position = pos;

        // Update ball velocity
        vel += acc * Time.fixedDeltaTime;
        if (vel.z < maxFallSpeed) {
            vel.z = maxFallSpeed;
        }

        // Update ball acceleration
        if (!rolling) {
            acc += grav * Time.fixedDeltaTime;
        }
        acc -= acc * friction * Time.fixedDeltaTime;
        if (acc.z < maxFallAcc) {
            acc.z = maxFallAcc;
        }
    }

    // For plunger
    public void Launch(float force) {
        ApplyForce(force * Vector3.forward);
        launched = true;
    }

    // Rudimentary, need to apply torque and not linear force
    public void Flip(float force, Vector3 jointPos) {
        Vector3 F = force * flipperNormal;
        Vector3 r = flipperPoint - jointPos;
        Vector3 torque = Vector3.Cross(r, F);
        ApplyForce(torque);
        flipped = true;
    }

    void ApplyForce(Vector3 force) {
        acc += force / mass;
        vel += acc;
    }

    void OnCollisionEnter(Collision collision) {
        GameObject obj = collision.gameObject;
        ContactPoint contact;
        Vector3 normal;

        for (int i = 0; i < collision.contactCount; i++) {
            contact = collision.GetContact(i);
            normal = contact.normal;

            // Prevent clipping
            if (contact.separation < radius) {
                transform.position = contact.point + normal * radius;
            }

            // Ball bounces off if it hits a bumper
            if (obj.CompareTag("Bumper")) {
                vel = Vector3.Reflect(vel, normal);
            }
            // Ball stops moving when it is on the plunger
            else if(obj.CompareTag("Plunger")) {
                vel = Vector3.zero;
                acc = Vector3.zero;
            }
            // Otherwise, movement in the direction of the obstacle is cancelled out; "equal and opposite reaction"
            else if (!obj.CompareTag("Untagged")) {
                vel -= Vector3.Dot(vel, normal) * normal;
                acc -= Vector3.Dot(acc, normal) * normal;
                if (obj.CompareTag("Flipper")) {
                    flipperNormal = normal;
                }
            }
        }
    }

    // For the scenarios where the obstacle is below the ball, so that it doesn't fall through
    void OnCollisionStay(Collision collision) {
        GameObject obj = collision.gameObject;
        rolling = true;

        // Prevent ball from falling through the plunger
        if (obj.CompareTag("Plunger") && !launched) {
            acc.z = 0;
            vel.z = 0;
        }
        // Roll along the wall
        else if (obj.CompareTag("Wall") || (obj.CompareTag("Flipper") && !flipped)) {
            Vector3 normal;   
            for (int i = 0; i < collision.contactCount; i++) {
                normal = collision.GetContact(i).normal;
                acc = Vector3.Cross(normal, Vector3.Cross(grav, normal));
                if (obj.CompareTag("Flipper")) {
                    flipperNormal = normal;
                    flipperPoint = collision.GetContact(i).point;
                }
            }
        }
    }

    void OnCollisionExit(Collision collision) {
        rolling = false;
        launched = false;
        flipped = false;
        flipperNormal = Vector3.zero;
        flipperPoint = Vector3.zero;
    }
}
