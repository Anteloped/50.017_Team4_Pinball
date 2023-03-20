using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInstance : MonoBehaviour
{
    public int scoreValue = 1;
    public float bumperForce = 500f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();    
            Vector3 bumperNormal = other.contacts[0].normal;
            
            ballRigidbody.AddForce(bumperNormal * bumperForce, ForceMode.Impulse);
            ScoreManager.instance.AddPoint();
            ballRigidbody.velocity = new Vector3(ballRigidbody.velocity.x, 0f, ballRigidbody.velocity.z);
        }
    }

    void Awake()
    {

    }
}
