using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaformGenerator : MonoBehaviour {

    public GameObject thePlatform; 
    public Transform generationPoint;
    private float distanceBetween;
    private float platformWidth;

    private float distanceBetweenMin;
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

    private float moveSpeed;


    // ********** SEULEMENT AVANT PREMIER FRAME **********
    void Start() {

        platformWidths = new float[theObjectPools.Length];

        for(int i = 0;i< theObjectPools.Length; i++){

            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

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

    // ********** CHOIX DE LA PLATEFORME ALEATOIREMENT VIA FCT **********
            if (firstRun)   // Pour le premier frame pas besoin de s'inquieter de la plateforme precedente      
            {
                firstRun = false;
                lastPlatform = UnityEngine.Random.Range(0, theObjectPools.Length);
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
            if(platformWidths[platformSelector] == 9)
            {
                distanceBetweenMax = Mathf.Exp(-heightChange / (float)2.10);
            }
            else if (heightChange >= -0.5)
            {
                distanceBetweenMax = Mathf.Exp(-heightChange/2);
            }
            else
            {
                distanceBetweenMax = Mathf.Exp(-heightChange/ (float)1.80);
            }

            // ********** CONDITIONS **********
            GameObject Player = GameObject.Find("Player");
            PlayerController playerController = Player.GetComponent<PlayerController>();
            moveSpeed = playerController.moveSpeed;        // Permet de recuperer la variable moveSpeed du script PlayerController. Avant la 1?re plateforme pour ne pas quelle soit coll?e au start.

            if (moveSpeed <= 12)
            {
                distanceBetweenMin = 2;
                distanceBetweenMax = 4;
            }
            else if (moveSpeed > 12 && moveSpeed <= 13.5)
            {
                distanceBetweenMin = 3;
                distanceBetweenMax = 5;
            }
            else if (moveSpeed > 13.5 && moveSpeed < 15)
            {
                distanceBetweenMin = 5;
                distanceBetweenMax = 8;
            }
            else
            {
                distanceBetweenMin = 6;
                distanceBetweenMax = 9;
            }

            if (previousPlatform == 3)
            {
                distanceBetween = (moveSpeed / 10) * Random.Range(4, 6);
            }
            else if (platformSelector == 3)
            {
                distanceBetweenMin = 4;
            }
            else
            {
                distanceBetween = (moveSpeed / 10) * Random.Range(distanceBetweenMin, distanceBetweenMax); // Distance aleatoire entre chaque plateforme proportionelle ? la vitesse.

            }

            if (platformSelector == 0){     // On specifie la future position lors de la generation

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, minHeight, transform.position.z);

            }
            else{
                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            }

            // ********** CONTRAINTES DE DISTANCE **********
            if (platformWidths[platformSelector] == 1)  // Contraintes pour la plateforme 3 (1x1)
            {
                distanceBetweenMin = (float)2.5;
            }
            else
            {
                distanceBetweenMin = 2;
            }

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
