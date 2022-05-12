using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaformGenerator : MonoBehaviour {

    public GameObject thePlatform; 
    public Transform generationPoint;
    private float distanceBetween;
    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    public ObjectPooler[] theObjectPools = null;   // on crée un array via les []

    // public GameObject[] thePlatforms; 
    private int platformSelector;
    private float[] platformWidths;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private int lastPlatform = 0;   // variable temporelle pour connaître dernière plateforme 
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
    int RandomWithExclusion (int min, int max, int exclusion)   //Empêche d'avoir deux fois la même plateforme de manière consécutive.
    {
        var result = UnityEngine.Random.Range(0, theObjectPools.Length -1);
        return (result < lastPlatform) ? result : result + 1;   // le - ? - agit comme un if avec un boolean: si la condion est true alors ça renvoie ça, sinon ça incrément le return de 1. 
    }

    // ********** RAFRAICHISSEMENT A CHAQUE FRAME **********
    void Update() {

        if (transform.position.x < generationPoint.position.x) {        // On s'assure que la génération se fasse plus loin

            // ********** CHOIX DE LA PLATEFORME ALEATOIREMENT VIA FCT **********
            if (firstRun)   // Pour le premier frame pas besoin de s'inquiéter de la plateforme précédente      
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

            // ********** CONDITIONS **********

            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax); // Distance aléatoire entre chaque plateforme

            if (platformSelector == 0){     // On spécifie la future position lors de la génération

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, minHeight, transform.position.z);

            }
            else{
                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            }

            // ********** GENERATION **********
            GameObject newPlatform = theObjectPools[lastPlatform].GetPooledObject();

            newPlatform.transform.position = transform.position;    // On génère la plateforme à la position calculée.
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

        }

    }
}
