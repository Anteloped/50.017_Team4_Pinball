using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltTable : MonoBehaviour
{
    [SerializeField] float tiltSpeed = 10.0f;
    [SerializeField] float tiltAngle = 10.0f;

    void TT(Vector3 tiltDirection)
    {
        transform.Rotate(tiltDirection * tiltSpeed * Time.deltaTime);
        float angle = transform.eulerAngles.z;
        if (angle > tiltAngle && angle < 90) {
            transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
        }
        else if (angle < 360 - tiltAngle && angle > 270) {
            transform.rotation = Quaternion.Euler(0, 0, 360 - tiltAngle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float angle = transform.eulerAngles.z;
        if (angle < tiltAngle + 10 || angle > 350 - tiltAngle) {
            if (Input.GetKey(KeyCode.Z))
            {
                TT(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.X))
            {
                TT(-Vector3.forward);
            }
        }
    }
}
