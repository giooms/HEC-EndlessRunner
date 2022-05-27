using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TempManager : MonoBehaviour
{
    public bool load_hall = false;
    public bool load_050 = false;
    public bool load_030 = false;
    public bool load_newbuild = false;
    public bool load_jardin = false;
    //public bool load_Cafet = false;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }


    // ********** BOUTONS CHOIX MAP **********
    public void ChooseMap_newbuild()
    {
        load_newbuild = true;
        load_050 = false;
        load_030 = false;
        load_hall = false;
        load_jardin = false;

    }
    public void ChooseMap_amphi050()
    {
        load_050 = true;
        load_newbuild = false;
        load_030 = false;
        load_hall = false;
        load_jardin = false;

    }
    public void ChooseMap_030()
    {
        load_030 = true;
        load_050 = false;
        load_newbuild = false;
        load_hall = false;
        load_jardin = false;

    }
    public void ChooseMap_hall()
    {
        load_hall = true;
        load_050 = false;
        load_newbuild = false;
        load_030 = false;
        load_jardin = false;

    }
    public void ChooseMap_jardin()
    {
        load_jardin = true;
        load_050 = false;
        load_newbuild = false;
        load_030 = false;
        load_hall = false;



    }
    public void ChooseMap_cafet()
    {
        
    }


    // ********** BOUTONS CHOIX AVATAR **********
    public void ChoosePlayer()
    {
        /* Cette fonction est appelée à chaque fois qu'un des bouttons Player
        (de PlayerGilles à PlayerNathan) a été cliqué.
        Il faut écrire dans cette fonction : Desactivate les Player qui n'ont pas été sélectionnés.
        Ensuite dans le script PlatformGenerator, il faudra aller chercher ligne 73 le nom du GameObject qui est actif. Autre possibilité, mettre les GameObject Player dans un parent, et aller chercher juste le parent dans le script ligne 73?? Vu qu'on prend juste les transform?? => surement le plus facile*/
    }

    
}
