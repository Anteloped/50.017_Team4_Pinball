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
    [SerializeField] FlipperTrigger trigger;
    Quaternion initialOrientation;
    Vector3 initialAngles;
    bool _lock = false;
    int timer = 0;
    int lockTimer = 60;
    
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

            if (!_lock && trigger.ball != null) {
                trigger.ball.AddForce(Vector3.forward * 2.5f, ForceMode.Impulse);
                _lock = true;
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

    void FixedUpdate() {
        if (_lock) {
            timer += 1;
            if (timer >= lockTimer) {
                timer = 0;
                _lock = false;
            }
        }
    }
}
