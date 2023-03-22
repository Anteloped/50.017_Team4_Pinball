using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 endGameCoord ;
    Vector3 pos;
    Vector3 vel = Vector3.zero;
    Vector3 acc = new Vector3(0, 0, -9.8f);
    static float mass = 1.0f;
    static float friction = 0.05f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (rb.velocity.y > 0) {
        //    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        //}

        if ((rb.position.z-endGameCoord.z)<=0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    /*
    void FixedUpdate() {
        transform.position += vel / 60;)
        //vel += acc / 60;
        vel -= vel * friction / 60;
    }
    */

    void ApplyForce(Vector3 force) {
        acc = force / mass;
    }
}
