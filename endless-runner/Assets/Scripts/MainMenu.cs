using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playGameLevel;

    public void PlayGame()
    {
        SceneManager.LoadScene(playGameLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChoosePlayer()
    {
        /* Cette fonction est appel�e � chaque fois qu'un des bouttons Player
        (de PlayerGilles � PlayerNathan) a �t� cliqu�.
        Il faut �crire dans cette fonction : Desactivate les Player qui n'ont pas �t� s�lectionn�s.
        Ensuite dans le script PlatformGenerator, il faudra aller chercher ligne 73 le nom du GameObject qui est actif. Autre possibilit�, mettre les GameObject Player dans un parent, et aller chercher juste le parent dans le script ligne 73?? Vu qu'on prend juste les transform?? => surement le plus facile*/
    }
}
