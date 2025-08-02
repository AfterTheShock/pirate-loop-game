using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class SetupPreviewObject : MonoBehaviour
{
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
            
            List<MeshRenderer> meshRenderersList = new List<MeshRenderer>();
            
            SearchChildMesh(transform, meshRenderersList);

#if UNITY_EDITOR
            EditorApplication.delayCall += () =>
            {
                DestroyImmediate(this);
            };
#endif
        }
    }

    private void SearchChildMesh(Transform parent, List<MeshRenderer> meshRenderersList)
    {
        foreach (Transform child in parent)
        {
            if (child.TryGetComponent(out MeshRenderer childMesh))
            {
                childMesh.material = ObjectPlacerSingleton.Instance.ObjectOverviewMaterial;
                childMesh.shadowCastingMode = ShadowCastingMode.Off;
                meshRenderersList.Add(childMesh);
            }
            else
            {
                SearchChildMesh(child, meshRenderersList);
            }
        }

        gameObject.GetComponent<PreviewObjectCheck>().SetPreviewMeshRenderers(meshRenderersList);
    }
}
