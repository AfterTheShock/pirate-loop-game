using UnityEngine;

public class WalkerInteractions : MonoBehaviour
{
    [SerializeField] private int minimumPoints;
    
    public void TakeDamage()
    {
        GameManager.Instance.GivePointsToPlayer(minimumPoints);
    }
}
