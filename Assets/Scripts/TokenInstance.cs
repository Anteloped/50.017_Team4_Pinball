using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInstance : MonoBehaviour
{
    [SerializeField] int scoreValue = 1;
    float bumperForce = 2f;
    bool bumpLock = false;
    float timer = 0f;
    float lockTimer = 0.25f;

    void Start() {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * 1.5f, scale.y, scale.z * 1.5f);
    }

    void FixedUpdate() {
        if (bumpLock) {
            timer += Time.fixedDeltaTime;
            if (timer >=lockTimer) {
                bumpLock = false;
                timer = 0f;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!bumpLock && other.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();    
            Vector3 bumperNormal = other.GetContact(0).normal;
            
            ballRigidbody.AddForce(-bumperNormal * bumperForce, ForceMode.Impulse);
            ScoreManager.instance.AddPoint(scoreValue);
            //ballRigidbody.velocity = new Vector3(ballRigidbody.velocity.x, 0f, ballRigidbody.velocity.z);
            bumpLock = true;
        }
    }
}
