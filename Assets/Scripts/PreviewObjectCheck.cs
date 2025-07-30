using System.Collections.Generic;
using UnityEngine;

public class PreviewObjectCheck : MonoBehaviour
{
    [SerializeField] private LayerMask invalidLayers;
    [SerializeField] List<Collider> collidingObjects = new List<Collider>();
    
    public bool IsValid { get; private set; } = true;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter collide");
        
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
