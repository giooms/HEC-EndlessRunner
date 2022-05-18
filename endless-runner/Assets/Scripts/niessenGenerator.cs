
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class niessenGenerator : MonoBehaviour
{
    public ObjectPooler niessenPools;

    public void creemechant(Vector3 startposition)
    {
        GameObject niessen = niessenPools.GetPooledObject();
        niessen.transform.position = startposition;
        niessen.SetActive(true);

    }
}
