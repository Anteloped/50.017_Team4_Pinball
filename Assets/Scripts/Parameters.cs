using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    private float bumperPower; // default 1.8f
    private float ballBounciness; // default 0.55f
    private float gravity; // default -10f

    // Start is called before the first frame update
    void Start()
    {
        // initialize values for the parameters
        bumperPower = 2f;
        gravity = -10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getBumperPower()
    {
        return this.bumperPower;
    }

    public float getBallBounciness()
    {
        return this.ballBounciness;
    }

    public float getGravity()
    {
        return this.gravity;
    }

    public void setBumperPower(float powerLevel)
    {
        this.bumperPower = powerLevel / 5f * 3f;
        GameObject.Find("PlayArea").GetComponent<NarrowPhase>().setBumperPower(this.bumperPower);

        Debug.Log(string.Format("Bumper Power changed to {0}", bumperPower));
    }

    public void setBallBounciness(float bouncinessLevel)
    {
        this.ballBounciness = (bouncinessLevel - 3f) / 10 + 0.55f;
        GameObject.Find("PlayArea").GetComponent<NarrowPhase>().setBallBounciness(this.ballBounciness);

        Debug.Log(string.Format("Ball Bounciness changed to {0}", ballBounciness));
    }

    public void setGravity(float gravityLevel)
    {
        this.gravity = - (gravityLevel - 3f) * 4f - 10f;
        GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>().setGravity(this.gravity);
        Debug.Log(string.Format("Gravity changed to {0}", gravity));
    }
}
