using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{

    public int numLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bottom")
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        numLives--;
        // Update UI to display new lives count
        if (numLives == 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        // Display game over screen
        // Reset game
    }


}
