using UnityEngine;
using UnityEngine.EventSystems;

public class CardsInHandVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering;

    private Vector3 targetPosition = Vector3.zero;

    [SerializeField] Vector3 hoveredTargetPosition = new Vector3(0,20,0);

    [SerializeField] float uppingSpeed = 10f;

    [SerializeField] float rotationMultiplierOnHover = 25f;
    [SerializeField] float tiltSpeed = 4f;
    [SerializeField] float idleTiltSpeed = 0.5f;
    [SerializeField] float targetSizeOnHover = 1.15f;
    [SerializeField] float targetSizeSpeed = 10f;
    [SerializeField] float idleMovementMultiplier = 3;

    private void Update()
    {
        if (isHovering)
        {
            HoverTilt();
        }
        else
        {
            IdleTilt();
        }

        ManageUpingOnHover();
        HoverSizeController();

    }

    private void ManageUpingOnHover()
    {
        this.transform.GetChild(0).localPosition = Vector3.Lerp(this.transform.GetChild(0).localPosition, targetPosition, uppingSpeed * Time.unscaledDeltaTime);
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

        Vector3 offset = (transform.GetChild(0).position - mousePosition) * rotationMultiplierOnHover;
        float tiltX = (offset.y / 2) * -1;
        float tiltY = offset.x;

        float lerpX = Mathf.LerpAngle(transform.GetChild(0).localEulerAngles.x, tiltX, tiltSpeed * Time.unscaledDeltaTime);
        float lerpY = Mathf.LerpAngle(transform.GetChild(0).localEulerAngles.y, tiltY, tiltSpeed * Time.unscaledDeltaTime);

        transform.GetChild(0).localEulerAngles = new Vector3(lerpX, lerpY, 0);

    }

    private void IdleTilt()
    {
        float sine = Mathf.Sin(Time.unscaledTime * idleTiltSpeed + transform.position.x * 100.3465f) * idleMovementMultiplier;
        float cosine = Mathf.Cos(Time.unscaledTime * idleTiltSpeed + transform.position.x * 102.5674f) * idleMovementMultiplier;

        float lerpX = Mathf.LerpAngle(transform.GetChild(0).localEulerAngles.x, sine, tiltSpeed * Time.unscaledDeltaTime);
        float lerpY = Mathf.LerpAngle(transform.GetChild(0).localEulerAngles.y, cosine, tiltSpeed * Time.unscaledDeltaTime);

        transform.GetChild(0).localEulerAngles = new Vector3(lerpX, lerpY, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        targetPosition = hoveredTargetPosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        targetPosition = Vector3.zero;
    }
}
