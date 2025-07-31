using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class SetupPreviewObject : MonoBehaviour
{
    [SerializeField] private List<Transform> childObjects = new List<Transform>();

    private void Awake()
    {
        if (TryGetComponent(out Rigidbody oldRigidbody))
        {
            oldRigidbody.isKinematic = true;
        }
        else
        {
            Rigidbody newRigidbody = gameObject.AddComponent<Rigidbody>();
            newRigidbody.isKinematic = true;
        }
        
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
        
        childObjects.Clear();
        
        foreach (Transform child in transform)
        {
            childObjects.Add(child);
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            meshRenderer.material = ObjectPlacerSingleton.Instance.ObjectOverviewMaterial;
            meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
        }
    }

}
