using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlaformGenerator : MonoBehaviour {

    public GameObject thePlatform;
    public Transform generationPoint;
    private float distanceBetween;
    private float platformWidth;

    private float distanceBetweenMin = 2;
    private float distanceBetweenMax;

    public ObjectPooler[] theObjectPools = null;   // on cree un array via les []

    // public GameObject[] thePlatforms; 
    private int platformSelector;
    private float[] platformWidths;
    private int previousPlatform;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private int lastPlatform = 0;   // variable temporelle pour connaitre derniere plateforme 
    private bool firstRun = true;

    private float moveSpeedInitial;
    private float moveSpeed;

    private float randomchance;// la proba d'avoir chaque mechant
    public niessenGenerator legenerateurdeniessen;
    public dupontGenerator legenerateurdedupont;
    public float mechants;// le pourcentage de chance d'avoir des mechants en fonction avancement
    public float vitessedepatrouille;
    public Vector3 startposition;

    public GameObject Background_hall;
    public GameObject Background_030;
    public GameObject Background_050;
    public GameObject Background_Cafet;
    public GameObject Background_jardin;
    public GameObject Background_newbuild;

    public bool load_hall;
    public bool load_050;
    public bool load_030;
    public bool load_newbuild;
    public bool load_jardin;
    public bool load_Cafet;

    public bool load_Player;
    public bool load_Thomas;
    public bool load_Marie;
    public bool load_Heloise;
    public bool load_Nathan;

    public GameObject Player;
    public GameObject Thomas;
    public GameObject Marie;
    public GameObject Heloise;
    public GameObject Nathan;

    // ********** SEULEMENT AVANT PREMIER FRAME **********
    void Start() {

        platformWidths = new float[theObjectPools.Length];

        for(int i = 0;i< theObjectPools.Length; i++){

            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        legenerateurdedupont = FindObjectOfType<dupontGenerator>();
        legenerateurdeniessen = FindObjectOfType<niessenGenerator>();

        // ********** CHANGEMENT MAP **********
                        //On charge toutes les variables de TempManager
        load_hall = TempManager.singleton.load_hall;
        load_050 = TempManager.singleton.load_050;
        load_030 = TempManager.singleton.load_030;
        load_Cafet = TempManager.singleton.load_Cafet;
        load_jardin = TempManager.singleton.load_jardin;
        load_newbuild = TempManager.singleton.load_newbuild;
        
                        //On set toutes maps en false
        Background_hall.SetActive(false);
        Background_030.SetActive(false);
        Background_050.SetActive(false);
        Background_Cafet.SetActive(false);
        Background_jardin.SetActive(false);
        Background_newbuild.SetActive(false);
        
                        //On set active la map dont la variable est true dans TempManager
        if (load_050)
        {
            Background_050.SetActive(true); 
        }
        else if (load_030)
        {
            Background_030.SetActive(true);
        }
        else if (load_Cafet)
        {
            Background_Cafet.SetActive(true);
        }
        else if (load_newbuild)
        {
            Background_newbuild.SetActive(true);
        }
        else if (load_jardin)
        {
            Background_jardin.SetActive(true);
        }
        else
        {
            Background_hall.SetActive(true);
        }

        // ********** CHANGEMENT AVATAR **********      //meme raisonnement que pour les maps
        load_Player = TempManager.singleton.load_Player;
        load_Thomas = TempManager.singleton.load_Thomas;
        load_Marie = TempManager.singleton.load_Marie;
        load_Heloise = TempManager.singleton.load_Heloise;
        load_Nathan = TempManager.singleton.load_Nathan;

        Player.SetActive(false);
        Thomas.SetActive(false);
        Marie.SetActive(false);
        Heloise.SetActive(false);
        Nathan.SetActive(false);

        if (load_Nathan)
        {
            Nathan.SetActive(true);
        }
        else if (load_Thomas)
        {
            Thomas.SetActive(true);
        }
        else if (load_Marie)
        {
            Marie.SetActive(true);
        }
        else if (load_Heloise)
        {
            Heloise.SetActive(true);
        }
        else
        {
            Player.SetActive(true);
        }
    }


    // ********** CONTRAINTE DE GENERATION ALEATOIRE **********
    int RandomWithExclusion (int min, int max, int exclusion)   //Empeche d'avoir deux fois la meme plateforme de maniere consecutive.
    {
        var result = UnityEngine.Random.Range(0, theObjectPools.Length -1);
        return (result < lastPlatform) ? result : result + 1;   // le - ? - agit comme un if avec un boolean: si la condion est true alors ca renvoie ca, sinon ca incremente le return de 1. 
    }

    // ********** RAFRAICHISSEMENT A CHAQUE FRAME **********
    void Update() {

        if (transform.position.x < generationPoint.position.x) {        // On s'assure que la generation se fasse plus loin


            if (load_Nathan)
            {
                GameObject Player = GameObject.Find("Nathan");
            }
            else if (load_Thomas)
            {
                GameObject Player = GameObject.Find("Thomas");
            }
            else if (load_Marie)
            {
                GameObject Player = GameObject.Find("Marie");
            }
            else if (load_Heloise)
            {
                GameObject Player = GameObject.Find("Heloise");
            }
            else
            {
                GameObject Player = GameObject.Find("Player");  
            }
            PlayerController playerController = Player.GetComponent<PlayerController>();


            // ********** CHOIX DE LA PLATEFORME ALEATOIREMENT VIA FCT **********
            if (firstRun)   // Pour le premier frame pas besoin de s'inquieter de la plateforme precedente      
            {
                firstRun = false;
                lastPlatform = UnityEngine.Random.Range(0, theObjectPools.Length);
                moveSpeedInitial = playerController.moveSpeed;

            }
            else
            {
                lastPlatform = RandomWithExclusion(0, theObjectPools.Length, lastPlatform);
            }

            platformSelector = lastPlatform;

            // ********** GESTION HAUTEUR **********
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight){
                
                    heightChange = maxHeight;
            }
            else if(heightChange < minHeight) {

                    heightChange = minHeight;
            }

            // ********** CALCUL DISTANCE MAX EN FONCTION DE LA HAUTEUR **********

            float DistanceMax(float palier) {
                if (platformWidths[platformSelector] == 9)
                {
                    distanceBetweenMax = 3;
                    return distanceBetweenMax;
                }
                else if (heightChange >= -0.5)
                {
                    //distanceBetweenMax = (moveSpeed / moveSpeedInitial) * Mathf.Exp(-heightChange / (float)2.05);
                    distanceBetweenMax = palier - ((float)0.9 * heightChange);
                    return distanceBetweenMax;
                }
                else
                {
                    //distanceBetweenMax = (moveSpeed / moveSpeedInitial) * Mathf.Exp(-heightChange / (float)1.80);
                    distanceBetweenMax = palier + ((float)0.25 * heightChange);
                    return distanceBetweenMax;
                }
            }

            // ********** CONDITIONS **********
            
            moveSpeed = playerController.moveSpeed; // Calcul du nouveau moveSpeed

            if (moveSpeed <= 12)
            {
                /*distanceBetweenMin = 2;
                distanceBetweenMax = 4;*/
                if (previousPlatform == 2 || previousPlatform == 3)
                {
                    distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(4,5);
                }
                else
                {
                    DistanceMax(4);
                    distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(distanceBetweenMin, distanceBetweenMax);
                }
            }
            else if (moveSpeed > 12 && moveSpeed <= 13.5)
            {
                /*distanceBetweenMin = 3;
                distanceBetweenMax = 5;*/
                DistanceMax(5);
                distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(((float)1.25 * distanceBetweenMin), distanceBetweenMax);
            }
            else if (moveSpeed > 13.5 && moveSpeed < 15)
            {
                /*distanceBetweenMin = 5;
                distanceBetweenMax = 8;*/
                if (previousPlatform == 2 || previousPlatform == 3)
                {
                    distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(6, 7);
                }
                else
                {
                    DistanceMax(6);
                    distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(distanceBetweenMin, distanceBetweenMax);
                }
                DistanceMax(6);
                distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(((float)1.85 * distanceBetweenMin), distanceBetweenMax);
            }
            else
            {
                //distanceBetweenMin = 6;
                //distanceBetweenMax = 9;
                DistanceMax((float)6.2);
                distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range((2 * distanceBetweenMin), distanceBetweenMax);
            }

            /*if (previousPlatform == 3)
            {
                distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(4, 6);
            }
            else if (platformSelector == 3)
            {
                distanceBetweenMin = 4;
            }
            else
            {
                distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(distanceBetweenMin, distanceBetweenMax); // Distance aleatoire entre chaque plateforme proportionelle ? la vitesse.

            }*/

            // ********** CONTRAINTES DE DISTANCE **********
            /*if (previousPlatform == 3 || previousPlatform == 4)  // Contraintes pour la plateforme 3 (1x1)
            {
                distanceBetweenMin = (float)3;
            }
            else
            {
                distanceBetweenMin = 2;
            }*/

            // distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(distanceBetweenMin, distanceBetweenMax);

            /*if (previousPlatform == 3)
            {
                distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(4, DistanceMax(6));
            }*/
            /*else if (platformSelector == 3)
            {
                distanceBetweenMin = 4;
            }*/
            /*else
            {
                distanceBetween = (moveSpeed / 10) * Random.Range(distanceBetweenMin, distanceBetweenMax); // Distance aleatoire entre chaque plateforme proportionelle ? la vitesse.

            }*/

            if (platformSelector == 0){     // On specifie la future position lors de la generation

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, minHeight, transform.position.z);

            }
            else{
                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            }

            // ********** LES MECHANTS **********
            randomchance = Random.Range(0.0f, 1.0f);
            if (platformWidths[platformSelector] == 9)
            {


                    if (randomchance < 0.6f)// parce qu'il est bien connu qu'on voit plus souvent monsieur dupont que monsieur niessen
                    {
                        legenerateurdedupont.creemechant(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z));
                    }
                    else
                    {
                        legenerateurdeniessen.creemechant(new Vector3(transform.position.x, transform.position.y + 2.2f, transform.position.z));

                    }
                }
            
            // ********** CONTRAINTES DE DISTANCE **********
   

            /*if (platformWidths[platformSelector] == 3 || platformWidths[platformSelector] == 1)  // Contraintes pour la plateforme 3 (1x1)
            {
                distanceBetweenMin = distanceBetweenMax;
            }
            else
            {
                distanceBetweenMin = 2;
            }*/

            // ********** GENERATION **********
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;    // On genere la plateforme a la position calculee.
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

            previousPlatform = platformSelector;

        }

    }
}
