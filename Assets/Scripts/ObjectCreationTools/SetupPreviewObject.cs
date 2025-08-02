using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class SetupPreviewObject : MonoBehaviour
{
    [SerializeField] private List<Transform> childObjects = new List<Transform>();

    [Conditional("UNITY_EDITOR")]
    private void Awake()
    {
        if (!Application.isPlaying)
        {
            MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
                if (script != this) DestroyImmediate(script);
        
            if (!TryGetComponent(out PreviewObjectCheck oldPreviewObjectCheck))
                gameObject.AddComponent<PreviewObjectCheck>();
        
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

#if UNITY_EDITOR
            EditorApplication.delayCall += () =>
            {
                DestroyImmediate(this);
            };
#endif
        }
    }

}
