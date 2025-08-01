using UnityEngine;
using UnityEngine.EventSystems;

public class CardVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [Header("idle")]
    [SerializeField] float idleMovementMultiplier = 3;

    [Header("OnHover")]
    private bool isHovering;
    [SerializeField] float rotationMultiplierOnHover = 0.25f;
    [SerializeField] float tiltSpeed = 1f;
    [SerializeField] float idleTiltSpeed = 0.5f;
    [SerializeField] float targetSizeOnHover = 1.35f;
    [SerializeField] float targetSizeSpeed = 1f;

    [Header("Movement")]
    [SerializeField] float movementLerpSpeed = 5f;

    private bool initializedCardStartPosition = false;

    private Transform childZero;

    CardDataManager cardDataManager;

    private float defaultTimeToShowDescription = 0.3f;
    private float timeLeftToShowDescription = 0.5f;
    private bool isDescriptionShown;

    private void Awake()
    {
        RectTransform initialPos = ShopManager.Instance.cardInitialPos;

        childZero = this.transform.GetChild(0);

        childZero.GetComponent<RectTransform>().position = initialPos.position;
        childZero.GetComponent<RectTransform>().eulerAngles = initialPos.eulerAngles + new Vector3(0, 15, 0);

        cardDataManager = this.transform.parent.GetComponent<CardDataManager>();
    }

    private void OnDestroy()
    {
        Destroy(childZero.gameObject);
    }

    private void Start()
    {
        this.transform.GetChild(0).SetParent(ShopManager.Instance.cardVisualsParent);
    }

    void Update()
    {
        if (isHovering)
        {
            HoverTilt();
        }
        else
        {
            IdleTilt();
        }

        HoverSizeController();

        ControlCardvisualsPosition();

        if (isHovering && timeLeftToShowDescription > 0) timeLeftToShowDescription -= Time.unscaledDeltaTime;

        if(isHovering && timeLeftToShowDescription <= 0 && !isDescriptionShown)
        {
            isDescriptionShown = true;
            cardDataManager.OnHoverOverCard();
        }
    }

    private void ControlCardvisualsPosition()
    {
        if (!initializedCardStartPosition)
        {
            RectTransform initialPos = ShopManager.Instance.cardInitialPos;
            childZero.GetComponent<RectTransform>().position = initialPos.position;
            childZero.GetComponent<RectTransform>().eulerAngles = initialPos.eulerAngles + new Vector3(0, 15, 0);

            initializedCardStartPosition = true;
        }
        else
            childZero.position = Vector3.Lerp(childZero.position, this.transform.position, movementLerpSpeed * Time.unscaledDeltaTime);
    }

    private void HoverSizeController()
    {
        float newSize;
        if (isHovering)
        {
            newSize = Mathf.Lerp(transform.parent.localScale.x, targetSizeOnHover, targetSizeSpeed * Time.unscaledDeltaTime * 3);
        }
        else
        {
            newSize = Mathf.Lerp(transform.parent.localScale.x, 1, targetSizeSpeed * Time.unscaledDeltaTime);
        }

        transform.parent.localScale = Vector3.one * newSize;
    }

    private void HoverTilt()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 1;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 offset = (childZero.position - mousePosition) * rotationMultiplierOnHover;
        float tiltX = (offset.y / 2) * -1;
        float tiltY = offset.x;

        float lerpX = Mathf.LerpAngle(childZero.localEulerAngles.x, tiltX , tiltSpeed * Time.unscaledDeltaTime);
        float lerpY = Mathf.LerpAngle(childZero.localEulerAngles.y, tiltY , tiltSpeed * Time.unscaledDeltaTime);

        childZero.localEulerAngles = new Vector3(lerpX, lerpY, 0);

    }

    private void IdleTilt()
    {
        float sine = Mathf.Sin(Time.unscaledTime * idleTiltSpeed + transform.position.x * 100.3465f) * idleMovementMultiplier;
        float cosine = Mathf.Cos(Time.unscaledTime * idleTiltSpeed + transform.position.x * 102.5674f) * idleMovementMultiplier;

        float lerpX = Mathf.LerpAngle(childZero.localEulerAngles.x, sine, tiltSpeed * Time.unscaledDeltaTime);
        float lerpY = Mathf.LerpAngle(childZero.localEulerAngles.y, cosine, tiltSpeed * Time.unscaledDeltaTime);

        childZero.localEulerAngles = new Vector3(lerpX, lerpY, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        timeLeftToShowDescription = defaultTimeToShowDescription;
        //cardDataManager.OnHoverOverCard();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        if (isDescriptionShown) cardDataManager.OnHoverOutsideCard();

        isDescriptionShown = false;
    }

}
