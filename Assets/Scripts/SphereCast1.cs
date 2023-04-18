using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCast1 : MonoBehaviour
{
    public bool willTunnel = false;
    float radius = 1.0f;
    Vector3 vel;

    public void ChangeScale(Vector3 v) {
        vel = v;
        radius = v.magnitude + 1.0f;
        transform.localScale = new Vector3(radius, radius, radius);
    }

    void CheckTunneling(Collider other) {
        if (!other.gameObject.CompareTag("Untagged") && !other.gameObject.CompareTag("Ball")) {
            Vector3 pos = transform.position;
            Vector3 nearSide = other.ClosestPointOnBounds(pos);
            Vector3 limit = pos + radius * (nearSide - pos).normalized; // vel
            Vector3 farSide = other.ClosestPointOnBounds(limit);
            if (nearSide == farSide || Vector3.Distance(pos, farSide) >= radius) {
                willTunnel = false;
            }
            else {
                willTunnel = true;
            }
        }
    }

    void onTriggerEnter(Collider other) {
        CheckTunneling(other);
    }

    void OnTriggerStay(Collider other) {
        CheckTunneling(other);
    }

    void onTriggerExit(Collider other) {
        if (!other.gameObject.CompareTag("Untagged")) {
            willTunnel = false;
        }
    }
}
