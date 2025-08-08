using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CheckIfHoldingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UnityEvent onHoldingEvent;
    [SerializeField] UnityEvent onNotHoldingEvent;

    bool isOnButton = false;

    private void Update()
    {
        if(isOnButton && Input.GetMouseButton(0)) onHoldingEvent.Invoke();
        else onNotHoldingEvent.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOnButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOnButton = false;
    }

}
