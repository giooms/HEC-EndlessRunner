using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // ********** BOUTONS CHOIX MAP **********
    public void MapSelect_newbuild()
    {
        TempManager.singleton.ChooseMap_newbuild();
    }
    public void MapSelect_050()
    {
        TempManager.singleton.ChooseMap_amphi050();
    }
    public void MapSelect_030()
    {
        TempManager.singleton.ChooseMap_030();
    }
    public void MapSelect_jardin()
    {
        TempManager.singleton.ChooseMap_jardin();
    }
    public void MapSelect_hall()
    {
        TempManager.singleton.ChooseMap_hall();
    }
    public void MapSelect_Cafet()
    {
        TempManager.singleton.ChooseMap_cafet();
    }

    // ********** BOUTONS CHOIX AVATAR **********
    public void AvatarSelect_Player()
    {
        TempManager.singleton.ChoosePlayer();
    }
    public void AvatarSelect_Thomas()
    {
        TempManager.singleton.ChooseThomas();
    }
    public void AvatarSelect_Marie()
    {
        TempManager.singleton.ChooseMarie();
    }
    public void AvatarSelect_Heloise()
    {
        TempManager.singleton.ChooseHeloise();
    }
    public void AvatarSelect_Nathan()
    {
        TempManager.singleton.ChooseNathan();
    }
}
 