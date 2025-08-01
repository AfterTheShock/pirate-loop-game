using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int pointsEarnedThisRound;

    [SerializeField] int pointsPerSecond = 1;

    private float timeSinceLastSecond;

    void Update()
    {
        timeSinceLastSecond += Time.deltaTime;

        if(timeSinceLastSecond >= 1)
        {
            timeSinceLastSecond = 0;

            pointsEarnedThisRound += pointsPerSecond;

            HudManager.Instance.SetPointsText(pointsEarnedThisRound);
        }
    }

    public void GivePointsToPlayer(int points)
    {
        pointsEarnedThisRound += points;

        HudManager.Instance.SetPointsText(pointsEarnedThisRound);
    }
}
