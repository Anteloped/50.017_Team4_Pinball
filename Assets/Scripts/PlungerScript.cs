using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlungerScript : MonoBehaviour
{
    float power;
    [SerializeField] float minPower = 0f;
    [SerializeField] float maxPower = 200f;
    [SerializeField] Slider powerSlider;
    Rigidbody ball;
    bool ballReady;

    void Start()
    {
        powerSlider.minValue = 0f;
        powerSlider.maxValue = maxPower;
    }

    void Update()
    {
        if (ballReady)
        {
            powerSlider.gameObject.SetActive(true);
        }
        else
        {
            powerSlider.gameObject.SetActive(false);
        }

        powerSlider.value = power;
        if(ball != null)
        {
            ballReady = true;
            if (Input.GetKey(KeyCode.Space))
            {
                if(power <= maxPower)
                {
                    power += 150 * Time.deltaTime;
                }
            }

            if(Input.GetKeyUp(KeyCode.Space))
            {
                if (power < minPower) {
                    power = minPower;
                }

                ball.AddForce(power * Vector3.forward);
            }
        }
        else
        {
            ballReady= false;
            power = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ball = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ball = null;
            power = 0f;
        }
    }
}


