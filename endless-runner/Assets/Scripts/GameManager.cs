using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public GameObject Player;
    public GameObject Thomas;
    public GameObject Marie;
    public GameObject Heloise;
    public GameObject Nathan;

    private GameObject ActivePlayer;
    private PlayerController thePlayer;
    private Vector3 playerStartPoint;

    private ObjectDestroyer[] platformList;

    private ScoreManager theScoreManager;

    public DeathMenu theDeathScreen;
    public GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        if (TempManager.singleton.load_Nathan)
        {
            ActivePlayer = GameObject.Find("Nathan");
        }
        else if (TempManager.singleton.load_Thomas)
        {
            ActivePlayer = GameObject.Find("Thomas");
        }
        else if (TempManager.singleton.load_Marie)
        {
            ActivePlayer = GameObject.Find("Marie");
        }
        else if (TempManager.singleton.load_Heloise)
        {
            ActivePlayer = GameObject.Find("Heloise");
        }
        else
        {
            ActivePlayer = GameObject.Find("Player");
        }
        thePlayer = ActivePlayer.GetComponent<PlayerController>();
        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayer.transform.position;

        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);

        theDeathScreen.gameObject.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Reset()
    {
        theDeathScreen.gameObject.SetActive(false);
        pauseButton.SetActive(true);

        platformList = FindObjectsOfType<ObjectDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);

        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }

}
