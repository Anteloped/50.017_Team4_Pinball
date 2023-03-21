using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] float maxAngle = 20.0f;
    [SerializeField] float flipTime = 1.0f;
    [SerializeField] string buttonName = "Fire1";
    [SerializeField] bool isLeftSide = false;
    Quaternion initialOrientation;
    Vector3 initialAngles;
    
    // Start is called before the first frame update
    void Start()
    {
        initialAngles = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(buttonName))
        {
            //Rotate the flipper
            transform.Rotate(Vector3.up * maxAngle / flipTime * Time.deltaTime);
            Vector3 angles = transform.rotation.eulerAngles;
            if (!isLeftSide && angles.y - initialAngles.y >= maxAngle)
            {
                transform.rotation = Quaternion.Euler(initialAngles + Vector3.up * maxAngle);
            }
            else if (isLeftSide && angles.y - initialAngles.y <= 360 + maxAngle) {
                transform.rotation = Quaternion.Euler(initialAngles + Vector3.up * (360 + maxAngle));
            }
        }
        else if (transform.rotation.eulerAngles.y != initialAngles.y) {
            transform.Rotate(Vector3.up * -maxAngle / flipTime * Time.deltaTime);
            Vector3 angles = transform.rotation.eulerAngles;
            if (isLeftSide && angles.y <= -maxAngle) {
                transform.rotation = Quaternion.Euler(initialAngles);
            }
            else if (!isLeftSide && angles.y >= 360 - maxAngle) {
                transform.rotation = Quaternion.Euler(initialAngles);
            }
        }
    }
}
