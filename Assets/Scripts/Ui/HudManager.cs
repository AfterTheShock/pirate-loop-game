using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class HudManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MoneyText;
    [SerializeField] TextMeshProUGUI PointsText;

    [SerializeField] LocalizedString pointsString = new LocalizedString();

    private static HudManager _instance;
    public static HudManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<HudManager>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        SetPointsText(0, GameManager.Instance.maxPointsToWinRound);
    }

    public void SetMoneyText(int money)
    {
        MoneyText.text = money + "$";
    }

    public void SetPointsText(int points, int maxPointsToWinRound)
    {
        if(LocalizationUtilities.CheckIfLocalizedStringIsAssinged(pointsString)) 
            PointsText.text = pointsString.GetLocalizedString() + ": " + points + " / " + maxPointsToWinRound;
        else
            PointsText.text = "Points: " + points + " / " + maxPointsToWinRound;
    }
}
