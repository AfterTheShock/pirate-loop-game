using UnityEngine;

public class SpikyRocksObject : MonoBehaviour
{
    [SerializeField] private float circleRadius = 0.25f;

    [SerializeField] int damagePerTick = 1;

    [SerializeField] float slowPercent = 0.15f;
    [SerializeField] float slowTime = 0.2f;
    [SerializeField] float damageTickSpeed = 0.2f;
    private float timeSinceDamaged = 0;

    private Transform walkerTransform;
    private FollowPointsAndMove walkerMovement;
    private WalkerInteractions walkerInteractions;

    private void Awake()
    {
        walkerMovement = FindFirstObjectByType<FollowPointsAndMove>();
        walkerInteractions = FindFirstObjectByType<WalkerInteractions>();
        walkerTransform = walkerMovement.transform;
    }


    void Update()
    {
        timeSinceDamaged += Time.deltaTime;

        if (timeSinceDamaged < damageTickSpeed) return;

        Vector3 distanceToWalker = walkerTransform.position - transform.position;

        if (distanceToWalker.magnitude < circleRadius)
        {
            timeSinceDamaged = 0;

            walkerMovement.SlowWalker(slowTime, slowPercent);

            walkerInteractions.TakeDamage(damagePerTick);
        }
    }
}
