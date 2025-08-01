using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int pointsEarnedThisRound;
    public int maxPointsToWinRound = 10;
    
    [SerializeField] int pointsPerSecond = 1;

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

        if(timeSinceLastSecond >= 1)
        {
            timeSinceLastSecond = 0;

            pointsEarnedThisRound += pointsPerSecond;

            HudManager.Instance.SetPointsText(pointsEarnedThisRound , maxPointsToWinRound);
        }
    }

    public void GivePointsToPlayer(int points)
    {
        pointsEarnedThisRound += points;

        HudManager.Instance.SetPointsText(pointsEarnedThisRound, maxPointsToWinRound);
    }
}
