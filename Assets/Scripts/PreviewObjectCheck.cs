using System;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObjectCheck : MonoBehaviour
{
    [SerializeField] List<Collider> collidingObjects = new List<Collider>();
    private LayerMask invalidLayers;
    
    public bool IsValid { get; private set; } = true;

    private void Awake()
    {
        invalidLayers = ObjectPlacerSingleton.Instance.InvalidLayers;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            collidingObjects.Add(other);
            IsValid = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            collidingObjects.Remove(other);
            IsValid = collidingObjects.Count <= 0;
        }
    }
}
