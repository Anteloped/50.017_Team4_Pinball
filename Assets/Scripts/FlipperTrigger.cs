using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperTrigger : MonoBehaviour
{
    public Rigidbody ball = null;

    void OnTriggerEnter(Collider other) {
        Debug.Log("enter");
        if (other.gameObject.CompareTag("Ball")) {
            Debug.Log("ball found");
            ball = other.GetComponent<Rigidbody>();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            ball = null;
        }
    }
}
