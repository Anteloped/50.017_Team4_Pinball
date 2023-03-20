using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] float maxAngle = 20.0f;
    [SerializeField] float flipTime = 1.0f;
    [SerializeField] string buttonName = "Fire1";
    Quaternion initialOrientation;
    
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
            transform.Rotate(Vector3.up * maxAngle / flipTime * Time.deltaTime);
            Vector3 angles = initialOrientation.eulerAngles;
            if(transform.rotation.eulerAngles.y - angles.y >= maxAngle)
            {
                Quaternion newOrientation = transform.rotation;
                newOrientation.eulerAngles = new Vector3(angles.x, angles.y + maxAngle, angles.z);
                transform.rotation = newOrientation;
            }
        }
    }
}
