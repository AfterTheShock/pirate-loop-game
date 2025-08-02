using System.Collections.Generic;
using UnityEngine;

public class PreviewObjectCheck : MonoBehaviour
{
    [SerializeField] private List<Collider> collidingObjects = new List<Collider>();
    [SerializeField] bool canOnlyBePlacedInPath;
    
    public List<MeshRenderer> PreviewObjectsMeshRenderers;
    
    public LayerMask invalidLayers = (1 << 7) + (1 << 8);

    public LayerMask pathLayer = 1 << 9;

    private bool isOnPath = false;

    public bool IsValid { get; private set; } = true;

    private void Start()
    {
        Validate();
    }

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

        //Check if entered path
        if (canOnlyBePlacedInPath)
        {
            if (((1 << other.gameObject.layer) & pathLayer) != 0)
            {
                isOnPath = true;
                Validate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            collidingObjects.Remove(other);
            Validate();
        }

        //Check if exited path
        if (canOnlyBePlacedInPath)
        {
            if (((1 << other.gameObject.layer) & pathLayer) != 0)
            {
                isOnPath = false;
                Validate();
            }
        }
    }
    
    private void Validate()
    {
        if(!canOnlyBePlacedInPath) IsValid = collidingObjects.Count <= 0;
        else
        {
            if (isOnPath && collidingObjects.Count <= 0) IsValid = true;
            else IsValid = false;
        }
    }
    
    public void SetPreviewMeshRenderers(List<MeshRenderer> meshRenderersList)
    {
        PreviewObjectsMeshRenderers = meshRenderersList;
    }
}
