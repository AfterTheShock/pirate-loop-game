using UnityEngine;

public class WalkerInteractions : MonoBehaviour
{
    [SerializeField] private int minimumPoints;
    
    public void TakeDamage(int damageTaken = 1)
    {
        GameManager.Instance.GivePointsToPlayer(damageTaken);
    }
}
