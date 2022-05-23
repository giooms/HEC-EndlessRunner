using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float randomchance;// la proba d'avoir chaque méchant
    public niessenGenerator legenerateurdeniessen;
    public dupontGenerator legenerateurdedupont;
    public float mechants;// le pourcentage de chance d'avoir des méchants en fonction avancement
    public float vitessedepatrouille;
    public Vector3 startposition;


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

            GameObject Player = GameObject.Find("Player");
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
                if (previousPlatform == 3)
                {
                    distanceBetween = (moveSpeed / moveSpeedInitial) * Random.Range(4, 6);
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
                if (previousPlatform == 3)
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
