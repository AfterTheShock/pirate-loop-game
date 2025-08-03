using UnityEngine;

public class WalkerInteractions : MonoBehaviour
{
    [SerializeField] private int minimumPoints;

    [SerializeField] GameObject hitSourcePrefab;

    public void TakeDamage(int damageTaken = 1)
    {
        if(GameManager.Instance) GameManager.Instance.GivePointsToPlayer(damageTaken);

        if (hitSourcePrefab)
        {
            GameObject hitSource = Instantiate(hitSourcePrefab);
            hitSource.GetComponent<AudioSource>().pitch = Random.Range(0.9f,1.1f);
        }
    }
}
