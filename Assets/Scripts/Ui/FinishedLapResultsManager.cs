using TMPro;
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

    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI moneyText;

    public void OpenResultsScreen()
    {
        //Calculate ammount of money to give based on points erned
        GameManager.Instance.GiveMoneyToPlayer(GameManager.Instance.pointsEarnedThisRound / 2, true);

        ObjectPlacerSingleton.Instance.CancelPlacementOfObject();
        this.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0f;
        //Turn down the players hand visuals
        handCardCanvasGroup.interactable = false;
        handCardCanvasGroup.alpha = 0;

        pointsText.text = "Points: " + GameManager.Instance.pointsEarnedThisRound + " / " + GameManager.Instance.maxPointsToWinRound;
        moneyText.text = "+" + GameManager.Instance.moneyEarnedThisRound + "$";
    }

    public void ClickShopButton()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        ShopManager.Instance.EnterShop();
        GameManager.Instance.AddOneLoop();
    }
}
