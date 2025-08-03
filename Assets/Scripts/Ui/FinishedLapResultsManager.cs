using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] GameObject shopButton;
    [SerializeField] GameObject toMenuButton;

    public void OpenResultsScreen()
    {
        CalculateMoney();

        ObjectPlacerSingleton.Instance.CancelPlacementOfObject();
        this.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0f;
        //Turn down the players hand visuals
        handCardCanvasGroup.interactable = false;
        handCardCanvasGroup.alpha = 0;

        if(GameManager.Instance.pointsEarnedThisRound < GameManager.Instance.maxPointsToWinRound)
        {
            pointsText.color = Color.red;
            shopButton.SetActive(false);
            toMenuButton.SetActive(true);
        }

        pointsText.text = "Points: " + GameManager.Instance.pointsEarnedThisRound + " / " + GameManager.Instance.maxPointsToWinRound;
        moneyText.text = "+" + GameManager.Instance.moneyEarnedThisRound + "$";
    }

    public void ClickShopButton()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        ShopManager.Instance.EnterShop();
        GameManager.Instance.AddOneLoop();
    }

    private void CalculateMoney()
    {
        float coefficient = 2.4f;

        if (GameManager.Instance.pointsEarnedThisRound > 140) coefficient = 3.2f;

        int ammountOfMoney = Mathf.RoundToInt(GameManager.Instance.pointsEarnedThisRound / coefficient);
        if (GameManager.Instance.loopsMade <= 5) ammountOfMoney += (int)(GameManager.Instance.pointsEarnedThisRound / 6);
        GameManager.Instance.GiveMoneyToPlayer(ammountOfMoney, true);
    }
    
    public void ToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
