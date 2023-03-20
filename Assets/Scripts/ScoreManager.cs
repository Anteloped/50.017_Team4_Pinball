using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

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

    public void AddPoint(int points=1)
    {
        score += points;
        scoreText.text = score.ToString() + " POINTS";
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
}
