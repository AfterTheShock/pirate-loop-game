using UnityEngine;

public class MoneyGeneratorObject : MonoBehaviour
{
    [SerializeField] float timeToGiveMoney = 10;
    private float timePassedSinceMoneyGiven = 0;

    [SerializeField] int moneyGivenPerTime = 3;

    [SerializeField] Animator animator;

    void Update()
    {
        timePassedSinceMoneyGiven += Time.deltaTime;

        if(timePassedSinceMoneyGiven >= timeToGiveMoney)
        {
            timePassedSinceMoneyGiven = 0;

            if(GameManager.Instance) GameManager.Instance.GiveMoneyToPlayer(moneyGivenPerTime);

            animator.Play("ChestOpen");
            if (GameManager.Instance) AudioManagerSingleton.Instance.PlaySound("Coin", this.transform);
        }
    }
}
