using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private GameObject objectPreview;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Color validColor;
    [SerializeField] private Color invalidColor;
    
    private bool inPlacementMode;
    private bool validPlacement;
    private GameObject previewObject;
    private MeshRenderer previewObjMeshRenderer;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!inPlacementMode)
            {
                EnterPlacementMode();
            }
            else
            {
                ExitPlacementMode();
            }
        }

        if (inPlacementMode)
        {
            UpdateCurrentPlacementPosition();

            if (CanPlaceObject())
                SetValidPreviewState();
            else
                SetInvalidPreviewState();
        }
    }

    private void EnterPlacementMode()
    {
        inPlacementMode = true;
        previewObject = Instantiate(objectPreview, MouseWorldPosition(), Quaternion.identity);
        previewObjMeshRenderer = previewObject.GetComponentInChildren<MeshRenderer>();
    }

    private void UpdateCurrentPlacementPosition()
    {
        previewObject.transform.position = MouseWorldPosition();

        if (Input.GetMouseButtonDown(0)) PlaceObject();
    }

    private void PlaceObject()
    {
        if (!validPlacement) return;
        
        Instantiate(objectToPlace, MouseWorldPosition(), Quaternion.identity);
            
        ExitPlacementMode();
    }
    
    private void ExitPlacementMode()
    {
        inPlacementMode = false;
        Destroy(previewObject);
    }

    private void SetValidPreviewState()
    {
        validPlacement = true;
        previewObjMeshRenderer.material.color = validColor;
    }

    private void SetInvalidPreviewState()
    {
        validPlacement = false;
        previewObjMeshRenderer.material.color = invalidColor;
    }
    
    private bool CanPlaceObject()
    {
        if (previewObject == null) return false;

        return previewObject.GetComponentInChildren<PreviewObjectCheck>().IsValid;
    }
    
    private Vector3 MouseWorldPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray positionToSpawn = Camera.main.ScreenPointToRay(mousePosition);
        Physics.Raycast(positionToSpawn, out RaycastHit hit, Mathf.Infinity, groundLayer);
        return hit.point;
    }
}
