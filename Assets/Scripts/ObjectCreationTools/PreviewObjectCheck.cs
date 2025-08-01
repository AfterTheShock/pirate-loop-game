using System;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObjectCheck : MonoBehaviour
{
    [SerializeField] private List<Collider> collidingObjects = new List<Collider>();
    public LayerMask invalidLayers;
    
    public bool IsValid { get; private set; } = true;

    private void Update()
    {
        if (collidingObjects.Count > 0)
        {
            for (int i = 0; i < collidingObjects.Count; i++) 
                if(collidingObjects[i] == null) 
                    collidingObjects.Remove(collidingObjects[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            collidingObjects.Add(other);
            Validate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            collidingObjects.Remove(other);
            Validate();
        }
    }
    
    private void Validate()
    {
        IsValid = collidingObjects.Count <= 0;
    }
}
