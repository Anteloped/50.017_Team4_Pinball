using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public float maxAngle = 20.0f;
    public float flipTime = 1.0f;
    public string buttonName = "Fire1";

    private Quaternion initialOrientation;
    
    // Start is called before the first frame update
    void Start()
    {
        initialOrientation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(buttonName))
        {
            //Rotate the flipper
            transform.RotateAround(Vector3.up, maxAngle*Mathf.Deg2Rad/flipTime * Time.deltaTime);
            if(transform.rotation.eulerAngles.y - initialOrientation.eulerAngles.y>= maxAngle)
            {
                Quaternion newOrientation = transform.rotation;
                newOrientation.eulerAngles = new Vector3(initialOrientation.eulerAngles.x, initialOrientation.eulerAngles.y + maxAngle, initialOrientation.eulerAngles.z);
                transform.rotation = newOrientation;
            }
        }
    }
}
