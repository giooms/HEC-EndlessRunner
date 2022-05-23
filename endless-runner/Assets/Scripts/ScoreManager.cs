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
    public PlayerController playercontroller;// les points bonus qui viennent de PlayerController qd on saute sur un mechant
    //public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        playercontroller=FindObjectOfType<PlayerController>();

        //GameObject Player = GameObject.Find("Player");
        //PlayerController playerController = Player.GetComponent<PlayerController>();
        //pointbonus = playerController.pointbonus;// pas 

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highscoreCount = PlayerPrefs.GetFloat("HighScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount = scoreCount + (pointsPerSecond * Time.deltaTime) /*+ playercontroller.pointbonus*/;
        }

        if (scoreCount > highscoreCount)
        {
            highscoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highscoreCount);// pas une bonne idéé => si tu veux sauvegarder juste à la fin car si dans update va prendre trop de place
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highscoreText.text = " High Score: " + Mathf.Round(highscoreCount);
    }
}
