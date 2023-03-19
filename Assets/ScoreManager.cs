using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    int score = 0;
    int highScore = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString() + " POINTS";
        highScoreText.text = "HIGH SCORE: " + highScore.ToString() + " POINTS";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString() + " POINTS";
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
}
