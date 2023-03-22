using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltTable : MonoBehaviour
{
    [SerializeField] float tiltSpeed = 45.0f;
    float tiltAngle = 5.0f;
    Vector3 rot;

    void Start() {
        rot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = transform.eulerAngles.z;
        if (angle < tiltAngle + 10 || angle > 350 - tiltAngle) {
            if (Input.GetKey(KeyCode.X))
            {
                TT(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                TT(-Vector3.forward);
            }
        }

        if (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.X)) {
            if (angle > 0.5f && angle < 90) {
                TT(-Vector3.forward);
            }
            else if (angle < 365.5f && angle > 270) {
                TT(Vector3.forward);
            }
        }
    }

    void TT(Vector3 tiltDirection)
    {
        transform.Rotate(tiltDirection * tiltSpeed * Time.deltaTime);
        float angle = transform.eulerAngles.z;
        if (angle > tiltAngle && angle < 90) {
            transform.rotation = Quaternion.Euler(rot.x, rot.y, tiltAngle);
        }
        else if (angle < 360 - tiltAngle && angle > 270) {
            transform.rotation = Quaternion.Euler(rot.x, rot.y, 360 - tiltAngle);
        }
    }
}
