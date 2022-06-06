using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class niessenGenerator : MonoBehaviour
{
    public ObjectPooler niessenPools;

    private float Score;
    private int al;

    public void creemechant(Vector3 startposition)
    {
        // Recuperation du score pour integrer la contrainte
        GameObject ScoreManager = GameObject.Find("ScoreManager");
        ScoreManager ScoreManagerScript = ScoreManager.GetComponent<ScoreManager>();
        Score = ScoreManagerScript.scoreCount;

        if(Score < 50){
            al = Random.Range(0, 4);
        }
        else if ((Score >= 50) && (Score < 100)){
           al = Random.Range(0, 3);
        }
        else if ((Score >= 100) && (Score < 150)){
            al = Random.Range(0, 2);
        }
        else{
            al = 1;
        }


        if (al == 1){
            GameObject niessen = niessenPools.GetPooledObject();
            niessen.transform.position = startposition;
            niessen.SetActive(true);
        }

    }
}
