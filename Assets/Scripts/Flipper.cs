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
    float flipTimer = 0.0f;
    
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
            flipTimer += Time.deltaTime;
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
            flipTimer = 0.0f;
            Vector3 angles = transform.rotation.eulerAngles;
            if (isLeftSide && angles.y <= -maxAngle) {
                transform.rotation = Quaternion.Euler(initialAngles);
            }
            else if (!isLeftSide && angles.y >= 360 - maxAngle) {
                transform.rotation = Quaternion.Euler(initialAngles);
            }
        }

        // Apply force to the ball if it is on the flipper
        if (Input.GetButtonDown(buttonName) && trigger.ball != null) {
            float timeMul = 1.0f - flipTimer / flipTime;
            if (timeMul > 0.0f) {
                trigger.ball.Flip(timeMul, transform.position);
            }
        }
    }
}
