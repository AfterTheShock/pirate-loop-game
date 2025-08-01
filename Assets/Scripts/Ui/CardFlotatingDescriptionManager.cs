using TMPro;
using UnityEngine;

public class CardFlotatingDescriptionManager : MonoBehaviour
{
    private static CardFlotatingDescriptionManager _instance;
    public static CardFlotatingDescriptionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<CardFlotatingDescriptionManager>();
            }

            return _instance;
        }
    }

    public TextMeshProUGUI descriptionText;

    public GameObject thisChild;

    public CanvasGroup thisCanvasGroup;

    [SerializeField] float alphaIncreaseSpeed;

    private void Awake()
    {
        thisChild = this.transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        thisChild.SetActive(false);
    }

    private void OnEnable()
    {
        thisCanvasGroup.alpha = 0f;
    }

    private void Update()
    {
        this.transform.position = Input.mousePosition;
        thisChild.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        thisChild.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        thisChild.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1);

        if (thisChild.activeInHierarchy && thisCanvasGroup.alpha < 1) thisCanvasGroup.alpha += Time.unscaledDeltaTime * alphaIncreaseSpeed;
        else thisCanvasGroup.alpha = 1;
    }
}
