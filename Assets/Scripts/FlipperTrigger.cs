using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperTrigger : MonoBehaviour
{
    public Ball ball = null;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            ball = other.GetComponent<Ball>();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            ball = null;
        }
    }
}
