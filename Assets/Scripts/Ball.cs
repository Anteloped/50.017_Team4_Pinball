using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 endGameCoord ;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y > 0) {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }

        if ((rb.position.z-endGameCoord.z)<=0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
