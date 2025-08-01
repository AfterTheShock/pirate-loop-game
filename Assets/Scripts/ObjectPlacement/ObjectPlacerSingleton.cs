using UnityEngine;

public class ObjectPlacerSingleton : MonoBehaviour
{
    [SerializeField] private PlacementObject objectToPlace;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Color validColor;
    [SerializeField] private Color invalidColor;
    
    public Material ObjectOverviewMaterial;
    
    private bool inPlacementMode;
    private bool validPlacement;
    
    private GameObject previewedObject;
    private MeshRenderer previewObjMeshRenderer; // no lo puede encontrar
    
    private const float PREVIEW_ROTATION_SPEED = 10f;

    private static ObjectPlacerSingleton _instance;

    private CardScriptableObject cardBeingPlacedScriptableObject;

    [SerializeField] CanvasGroup candsInHandCanvasGroup;
    [SerializeField] float cardsAlphaWhenPlacing = 0.35f;

    public static ObjectPlacerSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<ObjectPlacerSingleton>();
            }

            return _instance;
        }
    }

    private void Update()
    {
        if (inPlacementMode)
        {
            UpdateCurrentPlacementPosition();

            if (CanPlaceObject())
                SetValidPreviewState();
            else
                SetInvalidPreviewState();

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) CancelPlacementOfObject();
        }
    }

    public void CardToPlace(CardScriptableObject cardScriptableObject)
    {
        cardBeingPlacedScriptableObject = cardScriptableObject;

        objectToPlace = cardScriptableObject.placementObject;
        EnterPlacementMode();
    }
    
    private void EnterPlacementMode()
    {
        inPlacementMode = true;
        previewedObject = Instantiate(objectToPlace.objectPrefabPreview, MouseWorldPosition(), Quaternion.identity);
        previewObjMeshRenderer = previewedObject.GetComponentInChildren<MeshRenderer>();

        candsInHandCanvasGroup.alpha = cardsAlphaWhenPlacing;
        candsInHandCanvasGroup.interactable = false;
        candsInHandCanvasGroup.blocksRaycasts = false;
    }

    private void UpdateCurrentPlacementPosition()
    {
        float mouseScroll = Input.mouseScrollDelta.y;
        
        if (mouseScroll != 0)
            previewedObject.transform.Rotate(new Vector3(0f, mouseScroll * PREVIEW_ROTATION_SPEED, 0f));
        
        previewedObject.transform.position = MouseWorldPosition();
        

        if (Input.GetMouseButtonDown(0)) PlaceObject();
    }

    private void PlaceObject()
    {
        if (!validPlacement) return;
        
        Instantiate(objectToPlace.objectPrefab, previewedObject.transform.position, previewedObject.transform.rotation);
            
        ExitPlacementMode();
    }
    
    public void CancelPlacementOfObject()
    {
        if (!inPlacementMode) return;

        ShopManager.Instance.CardBought(cardBeingPlacedScriptableObject);

        ExitPlacementMode();
    }

    private void ExitPlacementMode()
    {
        inPlacementMode = false;
        objectToPlace = null;
        cardBeingPlacedScriptableObject = null;
        Destroy(previewedObject);

        candsInHandCanvasGroup.alpha = 1;
        candsInHandCanvasGroup.interactable = true;
        candsInHandCanvasGroup.blocksRaycasts = true;
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
        if (previewedObject == null) return false;

        return previewedObject.GetComponentInChildren<PreviewObjectCheck>().IsValid;
    }
    
    private Vector3 MouseWorldPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray positionToSpawn = Camera.main.ScreenPointToRay(mousePosition);
        Physics.Raycast(positionToSpawn, out RaycastHit hit, Mathf.Infinity, groundLayer);
        return hit.point;
    }
}
