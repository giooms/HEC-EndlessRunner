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

    public ObjectPooler[] theObjectPools = null;   // on cr?e un array via les []

    // public GameObject[] thePlatforms; 
    private int platformSelector;
    private float[] platformWidths;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private int lastPlatform = 0;   // variable temporelle pour conna?tre derni?re plateforme 
    private bool firstRun = true;


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
    int RandomWithExclusion (int min, int max, int exclusion)   //Emp?che d'avoir deux fois la m?me plateforme de mani?re cons?cutive.
    {
        var result = UnityEngine.Random.Range(0, theObjectPools.Length -1);
        return (result < lastPlatform) ? result : result + 1;   // le - ? - agit comme un if avec un boolean: si la condion est true alors ?a renvoie ?a, sinon ?a incr?ment le return de 1. 
    }

    // ********** RAFRAICHISSEMENT A CHAQUE FRAME **********
    void Update() {

        if (transform.position.x < generationPoint.position.x) {        // On s'assure que la g?n?ration se fasse plus loin

            // ********** CHOIX DE LA PLATEFORME ALEATOIREMENT VIA FCT **********
            if (firstRun)   // Pour le premier frame pas besoin de s'inqui?ter de la plateforme pr?c?dente      
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
                distanceBetweenMax = Mathf.Exp(-heightChange/ (float)1.75);
            }

            // ********** CONDITIONS **********

            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax); // Distance al?atoire entre chaque plateforme

            if (platformSelector == 0){     // On sp?cifie la future position lors de la g?n?ration

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, minHeight, transform.position.z);

            }
            else{
                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            }

            // ********** DISTANCE MIN = 2 SI LASTPLATFORM WIDTH = 1 **********

            if (platformWidths[platformSelector] == 1)
            {
                distanceBetweenMin = 2;
            }
            else
            {
                distanceBetweenMin = 1;
            }

            // ********** GENERATION **********
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;    // On g?n?re la plateforme ? la position calcul?e.
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

        }

    }
}
