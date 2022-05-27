using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playGameLevel;
    public string chooseAvatar;
    public string chooseMap;

    public void PlayGame()
    {
        SceneManager.LoadScene(playGameLevel);
    }

    public void Map()
    {
        SceneManager.LoadScene(chooseMap);
    }

    public void Avatar()
    {
        SceneManager.LoadScene(chooseAvatar);
    }

}
