using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dupontGenerator : MonoBehaviour
{
    public ObjectPooler dupontPools;

    public void creemechant(Vector3 startposition)
    {
        GameObject dupont = dupontPools.GetPooledObject();
        dupont.transform.position = startposition;
        dupont.SetActive(true);
    }
}
