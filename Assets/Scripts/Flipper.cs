using System.Collections;
using System.Collections.Generic;
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
    float timer = 0f;
    float lockTimer = 0.5f;
    
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
            // Rotate the flipper
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
        // Return flipper to default position
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

        // Apply force to the ball if it is on the flipper
        if (Input.GetButtonDown(buttonName) && !_lock && trigger.ball != null) {
            trigger.ball.ApplyForce(Vector3.forward * 1f);
            _lock = true;
        }
    }

    void FixedUpdate() {
        // Timed lock to prevent force from being applied repeatedly every frame
        if (_lock) {
            timer += Time.fixedDeltaTime;
            if (timer >= lockTimer) {
                timer = 0f;
                _lock = false;
            }
        }
    }
}
