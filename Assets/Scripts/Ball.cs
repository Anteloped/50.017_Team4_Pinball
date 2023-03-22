using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    Vector3 vel;
    Vector3 acc = Vector3.zero;
    Vector3 grav;
    [SerializeField] Vector3 endGameCoord;
    [SerializeField] float gravity = -9.8f;
    static float mass = 0.08f;
    static float friction = 0.1f;
    [SerializeField] float tableAngle = 7f;
    [SerializeField] Transform table;
    bool rolling = false;

    void Start() {
        gravity *= Mathf.Sin(Mathf.Deg2Rad * tableAngle);
        grav = new Vector3(0, 0, gravity);
        vel = new Vector3(0, 0, -1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.z - endGameCoord.z) <= 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void FixedUpdate() {
        grav = new Vector3(gravity * Mathf.Sin(-table.rotation.eulerAngles.z * Mathf.Deg2Rad), 0, gravity);

        Vector3 pos = transform.position;
        pos += vel * Time.fixedDeltaTime + acc * Mathf.Pow(Time.deltaTime, 2) / 2;
        pos.x = Mathf.Clamp(pos.x, -2.6f, 3.2f);
        pos.y = 0.16f;
        pos.z = Mathf.Clamp(pos.z, endGameCoord.z - 1, 6.1f);
        transform.position = pos;

        vel += acc * Time.fixedDeltaTime;
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
        Vector3 normal = collision.GetContact(0).normal;
        if (collision.gameObject.CompareTag("Wall")) {
            vel -= Vector3.Dot(vel, normal) * normal;
            acc -= Vector3.Dot(acc, normal) * normal;
            //transform.position = collision.GetContact(0).point;
        }
        else if (collision.gameObject.CompareTag("Bumper")) {
            vel = Vector3.Reflect(vel, normal);
        }
    }

    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            Vector3 normal = collision.GetContact(0).normal;
            if (normal == Vector3.forward) {
                acc = Vector3.zero;
            }
            else {
                // roll along the wall
                acc = Vector3.Cross(normal, Vector3.Cross(grav, normal));
                rolling = true;
            }
        }
    }

    void OnCollisionExit(Collision collision) {
        rolling = false;
    }
}
