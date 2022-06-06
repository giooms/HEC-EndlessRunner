using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;

    public float scoreCount;
    public float highscoreCount;

    public float pointsPerSecond;

    public bool scoreIncreasing;

    public PlayerController pointbonustete;     // les points bonus qui viennent de patrouille qd on saute sur un mechant

    // Start is called before the first frame update
    void Start()
    {
        pointbonustete=FindObjectOfType<PlayerController>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highscoreCount = PlayerPrefs.GetFloat("HighScore");
            highscoreText.text = " Highest Grades Ever: " + Mathf.Round(highscoreCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

        scoreText.text = "Grades: " + Mathf.Round(scoreCount);
    }

    public void HighScore()
    {
        if (scoreCount > highscoreCount)
        {
            highscoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highscoreCount);
            highscoreText.text = " Highest Grades Ever: " + Mathf.Round(highscoreCount);
        }
    }
}