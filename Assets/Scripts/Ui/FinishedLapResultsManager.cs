using UnityEngine;

public class FinishedLapResultsManager : MonoBehaviour
{
    [SerializeField] CanvasGroup handCardCanvasGroup;

    private static FinishedLapResultsManager _instance;
    public static FinishedLapResultsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<FinishedLapResultsManager>();
            }

            return _instance;
        }
    }

    public void OpenResultsScreen()
    {
        ObjectPlacerSingleton.Instance.CancelPlacementOfObject();
        this.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0f;
        //Turn down the players hand visuals
        handCardCanvasGroup.interactable = false;
        handCardCanvasGroup.alpha = 0;
    }

    public void ClickShopButton()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        ShopManager.Instance.EnterShop();
    }
}
