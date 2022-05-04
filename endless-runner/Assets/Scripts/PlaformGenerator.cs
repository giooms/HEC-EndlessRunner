using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaformGenerator : MonoBehaviour {

    public GameObject thePlatform; 
    public Transform generationPoint;
    public float distanceBetween;
    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    public ObjectPooler[] theObjectPools;

    // public GameObject[] thePlatforms; // on crée un array via les []
    private int platformSelector;
    private float[] platformWidths;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private int lastPlatform;   // variable temporelle pour connaître dernière plateforme 

    

    // Start is called before the first frame update
    void Start() {

        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[theObjectPools.Length];

        for(int i = 0;i< theObjectPools.Length; i++){

            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

    }

    // Update is called once per frame
    void Update() {

        if (transform.position.x < generationPoint.position.x) {

            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);



            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight){
                
                    heightChange = maxHeight;
            }
            else if(heightChange < minHeight) {

                    heightChange = minHeight;
            }

            //Instantiate(/*thePlatform,*/ thePlatforms[platformSelector], transform.position, transform.rotation);

            if(platformSelector == 0){

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, minHeight, transform.position.z);

                GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

                newPlatform.transform.position = transform.position;
                newPlatform.transform.rotation = transform.rotation;
                newPlatform.SetActive(true);

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

            }
            else{
                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

                GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

                newPlatform.transform.position = transform.position;
                newPlatform.transform.rotation = transform.rotation;
                newPlatform.SetActive(true);

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

            }


        }

    }
}
