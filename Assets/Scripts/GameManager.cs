using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int pointsEarnedThisRound;
    public int maxPointsToWinRound = 20;

    public int currentAmmountOfMoney = 5;
    public int moneyEarnedThisRound = 0;

    public int loopsMade = 0;

    [SerializeField] int pointsPerTick = 1;
    [SerializeField] float intervalToGivePoints = 1;

    private float timeSinceLastSecond;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<GameManager>();
            }

            return _instance;
        }
    }
    
    void Update()
    {
        timeSinceLastSecond += Time.deltaTime;

        if(timeSinceLastSecond >= intervalToGivePoints)
        {
            timeSinceLastSecond = 0;

            pointsEarnedThisRound += pointsPerTick;

            HudManager.Instance.SetPointsText(pointsEarnedThisRound , maxPointsToWinRound);
        }

        if(Time.timeScale > 0)
        {
            if (Input.GetKey(KeyCode.Space)) Time.timeScale = 3;
            else Time.timeScale = 1;
        }
    }

    public void GivePointsToPlayer(int points)
    {
        pointsEarnedThisRound += points;

        HudManager.Instance.SetPointsText(pointsEarnedThisRound, maxPointsToWinRound);
    }

    public void GiveMoneyToPlayer(int moneyToGive, bool moneyGivenInsideOfRound = false)
    {
        currentAmmountOfMoney += moneyToGive;
        if(moneyGivenInsideOfRound) moneyEarnedThisRound += moneyToGive;

        HudManager.Instance.SetMoneyText(currentAmmountOfMoney);
    }

    public void AddOneLoop()
    {
        this.loopsMade++;

        maxPointsToWinRound += loopsMade * 3;

        pointsEarnedThisRound = 0;
        moneyEarnedThisRound = 0;

        HudManager.Instance.SetPointsText(pointsEarnedThisRound, maxPointsToWinRound);
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
    }
}
