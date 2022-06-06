using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempManager : MonoBehaviour
{
    public bool load_hall = false;
    public bool load_050 = false;
    public bool load_030 = false;
    public bool load_newbuild = false;
    public bool load_jardin = false;
    public bool load_Cafet = false;

    public bool load_Player = false;
    public bool load_Thomas = false;
    public bool load_Marie = false;
    public bool load_Heloise = false;
    public bool load_Nathan = false;


    public static TempManager singleton; //


    // Start is called before the first frame update
    void Awake()
    {
        if(singleton != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            singleton = this;
        }
    }
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
        load_Cafet = false;

    }
    public void ChooseMap_hall()
    {
        load_hall = true;
        load_050 = false;
        load_newbuild = false;
        load_030 = false;
        load_jardin = false;
        load_Cafet = false;

    }
    public void ChooseMap_jardin()
    {
        load_jardin = true;
        load_050 = false;
        load_newbuild = false;
        load_030 = false;
        load_hall = false;
        load_Cafet = false;
    }
    public void ChooseMap_cafet()
    {
        load_Cafet = true;
        load_jardin = false;
        load_050 = false;
        load_newbuild = false;
        load_030 = false;
        load_hall = false;
    }


    // ********** BOUTONS CHOIX AVATAR **********
    public void ChoosePlayer()
    {
        load_Player = true;
        load_Thomas = false;
        load_Marie = false;
        load_Heloise = false;
        load_Nathan = false;
    }
    public void ChooseThomas()
    {
        load_Player = false;
        load_Thomas = true;
        load_Marie = false;
        load_Heloise = false;
        load_Nathan = false;
    }
    public void ChooseMarie()
    {
        load_Player = false;
        load_Thomas = false;
        load_Marie = true;
        load_Heloise = false;
        load_Nathan = false;
    }
    public void ChooseHeloise()
    {
        load_Player = false;
        load_Thomas = false;
        load_Marie = false;
        load_Heloise = true;
        load_Nathan = false;
    }
    public void ChooseNathan()
    {
        load_Player = false;
        load_Thomas = false;
        load_Marie = false;
        load_Heloise = false;
        load_Nathan = true;
    }

}
