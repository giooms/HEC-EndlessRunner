using System.Collections;
using UnityEngine;
using System.Collections.Generic;       //pour utiliser les listes

public class ObjectPooler : MonoBehaviour{

    public GameObject pooledObject;
    public int pooledAmount;

    List<GameObject> pooledObjects;

    // Start is called before the first frame update
    void Start(){

        pooledObjects = new List<GameObject>();

        for(int i = 0; i < pooledAmount; i++){  //fonctionne à l'infini

            GameObject obj = (GameObject)Instantiate(pooledObject); //on convertit en GameObject pour être sûr que ça rentre dans la liste
            obj.SetActive(false);
            pooledObjects.Add(obj);     //L'objet est maintenant dans la liste

        }   
    }

    public GameObject GetPooledObject() {    //On crée une nouvelle fonction

        for (int i = 0; i < pooledObjects.Count; i++){

            if (!pooledObjects[i].activeInHierarchy){
                return pooledObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;

    }
}
