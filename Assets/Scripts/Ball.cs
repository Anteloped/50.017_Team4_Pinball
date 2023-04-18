using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class Ball : MonoBehaviour
{
    //[SerializeField] Vector3 endGameCoord;
    float maxSpeed = 10f;
    float maxFallSpeed = -10f;
    float maxAcc = 10f;
    float maxFallAcc = -10f;
    public float gravity = -10f;
    public float tableAngle = 7f; // the angle at which the table slants forward
    public Transform table;
    //[SerializeField] int lives = 3;

    static float mass = 0.08f;
    static float friction = 0.1f;
    static float radius = 0.16f;
    static float flipperLength = 0.86f;
    Vector3 vel;
    Vector3 acc;
    Vector3 grav;
    bool rolling = false;
    bool launched = false;
    Vector3 flipperPoint;
    Vector3 flipperNormal;
    //Vector3 startPos;
    //SphereCast1 sc;

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
        //sc = GetComponentsInChildren<SphereCast1>()[0];
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
        grav.x = gravity * Mathf.Sin(-table.rotation.eulerAngles.z * Mathf.Deg2Rad);

        // Update ball acceleration
        if (!rolling) {
            acc += grav * Time.fixedDeltaTime;
        }
        acc -= acc * friction * Time.fixedDeltaTime;
        acc.x = Mathf.Clamp(acc.x, -maxAcc, maxAcc);
        acc.z = Mathf.Clamp(acc.z, -maxAcc, maxAcc);
        /*if (acc.z < maxFallAcc) {
            acc.z = maxFallAcc;
        }*/

        // Update ball velocity
        vel += acc * Time.fixedDeltaTime;
        vel.x = Mathf.Clamp(vel.x, -maxSpeed, maxSpeed);
        vel.z = Mathf.Clamp(vel.z, -maxSpeed, maxSpeed);
        /*if (vel.z < maxFallSpeed) {
            vel.z = maxFallSpeed;
        }*/

        // Update ball position
        Vector3 pos = transform.position;
        pos += vel * Time.fixedDeltaTime; // + acc * Mathf.Pow(Time.deltaTime, 2) / 2;
        pos.y = radius;
        transform.position = pos;

        //sc.ChangeScale(vel * Time.fixedDeltaTime);
    }

    // For plunger
    public void Launch(float force) {
        ApplyForce(force * Vector3.forward);
        launched = true;
    }

    // For flipper
    // Need to prevent ball from clipping through during flipping motion
    public void Flip(float timeMul, Vector3 jointPos) {
        Vector3 torque = 1.25f * Vector3.forward; //flipperNormal.normalized;
        float spaceMul = (flipperPoint - jointPos).magnitude / flipperLength;
        torque *= timeMul * spaceMul;
        ApplyForce(torque);
    }

    void ApplyForce(Vector3 force) {
        acc += force / mass;
        acc.x = Mathf.Clamp(acc.x, -maxAcc, maxAcc);
        acc.z = Mathf.Clamp(acc.z, -maxAcc, maxAcc);
        vel += acc;
        vel.x = Mathf.Clamp(vel.x, -maxSpeed, maxSpeed);
        vel.z = Mathf.Clamp(vel.z, -maxSpeed, maxSpeed);
        transform.position += Vector3.forward * Time.fixedDeltaTime;
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
                //vel = Vector3.Reflect(vel, normal);
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
                    flipperPoint = contact.point;
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
        else if (obj.CompareTag("Wall") || (obj.CompareTag("Flipper"))) {
            Vector3 normal;   
            for (int i = 0; i < collision.contactCount; i++) {
                ContactPoint contact = collision.GetContact(i);
                normal = contact.normal;

                // Prevent clipping
                if (contact.separation < radius) {
                    transform.position = contact.point + normal * radius;
                }
                //normal = collision.GetContact(i).normal;
                acc = Vector3.Cross(normal, Vector3.Cross(grav, normal));
                if (obj.CompareTag("Flipper")) {
                    flipperPoint = collision.GetContact(i).point;
                    flipperNormal = normal;
                }
            }
        }
    }

    void OnCollisionExit(Collision collision) {
        rolling = false;
        launched = false;
    }

    public Vector3 getVelocity()
    {
        return vel;
    }

    public void setVelocity(Vector3 velocity)
    {
        this.vel = velocity;
    }
}
