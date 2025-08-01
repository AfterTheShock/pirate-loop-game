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

    public void SetMoneyText(int money)
    {
        MoneyText.text = money + "$";
    }

    public void SetPointsText(int points)
    {
        PointsText.text = "Points:" + points;
    }
}
