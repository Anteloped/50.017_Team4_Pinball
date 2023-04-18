using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    private float bumperPower;

    // Start is called before the first frame update
    void Start()
    {
        // initialize values for the parameters
        bumperPower = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getBumperPower()
    {
        return this.bumperPower;
    }

    public void setBumperPower(float power)
    {
        this.bumperPower = power;
    }
}
