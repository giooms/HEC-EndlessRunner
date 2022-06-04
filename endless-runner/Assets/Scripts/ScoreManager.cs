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
    //public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        pointbonustete=FindObjectOfType<PlayerController>();

        //GameObject Player = GameObject.Find("Player");
        //PlayerController playerController = Player.GetComponent<PlayerController>();
        //pointbonus = playerController.pointbonus;// pas 

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highscoreCount = PlayerPrefs.GetFloat("HighScore");
            highscoreText.text = " High Score: " + Mathf.Round(highscoreCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }

    public void HighScore()
    {
        if (scoreCount > highscoreCount)
        {
            highscoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highscoreCount);
            highscoreText.text = " High Score: " + Mathf.Round(highscoreCount);
        }
    }
}