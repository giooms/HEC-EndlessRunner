using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dupontGenerator : MonoBehaviour
{
    public ObjectPooler dupontPools;

    private float Score;
    private int al;

    public void creemechant(Vector3 startposition)
    {

        // Recuperation du score pour integrer la contrainte
        GameObject ScoreManager = GameObject.Find("ScoreManager");
        ScoreManager ScoreManagerScript = ScoreManager.GetComponent<ScoreManager>();
        Score = ScoreManagerScript.scoreCount;

        if (Score < 50)
        {
            al = Random.Range(1, 3);
        }
        else if ((Score >= 50) && (Score < 100))
        {
            al = Random.Range(1, 2);
        }
        else
        {
            al = 1;
        }

        if (al == 1)
        {
            GameObject dupont = dupontPools.GetPooledObject();
            dupont.transform.position = startposition;
            dupont.SetActive(true);
        }
    }
}
