using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public string mainMenuLevel;

    public void RestartGame()// fonction qui induit le redemarrage du jeu
    {
        FindObjectOfType<GameManager>().Reset();
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene("chooseAvatar");
    }
}