using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MoneyText;
    [SerializeField] TextMeshProUGUI PointsText;

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
        PointsText.text = "Points: " + points + " / " + maxPointsToWinRound;
    }
}
