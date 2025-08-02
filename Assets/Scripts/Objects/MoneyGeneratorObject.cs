using UnityEngine;

public class MoneyGeneratorObject : MonoBehaviour
{
    [SerializeField] float timeToGiveMoney = 10;
    private float timePassedSinceMoneyGiven = 0;

    [SerializeField] int moneyGivenPerTime = 3;

    void Update()
    {
        timePassedSinceMoneyGiven += Time.deltaTime;

        if(timePassedSinceMoneyGiven >= timeToGiveMoney)
        {
            timePassedSinceMoneyGiven = 0;

            GameManager.Instance.GiveMoneyToPlayer(moneyGivenPerTime);
        }
    }
}
