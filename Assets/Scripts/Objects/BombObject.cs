using System;
using System.Collections.Generic;
using UnityEngine;

public class BombObject : MonoBehaviour
{
    [SerializeField] private float circleRadius;
    
    private void Start()
    {
        GameObject[] buildingsToDestroy = GameObject.FindGameObjectsWithTag("Building");
        
        foreach (GameObject obj in buildingsToDestroy)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist <= circleRadius)
                DestroyBuilding(obj);
        }
    }

    private void DestroyBuilding(GameObject building)
    {
        Destroy(building);
    }
}