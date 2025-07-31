using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PreviewObjectCheck : MonoBehaviour
{
    private List<Collider> collidingObjects = new List<Collider>();
    private LayerMask invalidLayers;
    
    private MeshRenderer meshRenderer;
    private Rigidbody rigidBody;
    private Collider collider;
    
    public bool IsValid { get; private set; } = true;

    private void Awake()
    {
        PreviewObjectSetup();
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

    //Lo único que se necesita para que funcione es el script
    private void PreviewObjectSetup()
    {
        invalidLayers = ObjectPlacerSingleton.Instance.InvalidLayers;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = ObjectPlacerSingleton.Instance.ObjectOverviewMaterial;
        meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
        
        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.isKinematic = true;
        
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }
}
