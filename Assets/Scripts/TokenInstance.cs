using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInstance : MonoBehaviour
{
    [SerializeField] int scoreValue = 1;
    float bumperForce = 10f;
    bool bumpLock = false;
    float timer = 0.0f;
    float lockTimer = 1.0f;

    void Update() {
        if (bumpLock) {
            timer += Time.deltaTime;
            if (timer > lockTimer) {
                bumpLock = false;
                timer = 0.0f;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!bumpLock && other.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();    
            Vector3 bumperNormal = other.contacts[0].normal;
            
            ballRigidbody.AddForce(bumperNormal * bumperForce, ForceMode.Impulse);
            ScoreManager.instance.AddPoint(scoreValue);
            //ballRigidbody.velocity = new Vector3(ballRigidbody.velocity.x, 0f, ballRigidbody.velocity.z);
            bumpLock = true;
        }
    }
}
