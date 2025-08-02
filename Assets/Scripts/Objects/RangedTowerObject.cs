using UnityEngine;

public class RangedTowerObject : MonoBehaviour
{
    [Header("Stun Area")]
    [SerializeField] private float circleRadius = 8;

    [Header("Effects")]
    [SerializeField] private int damage = 5;
    [SerializeField] private float hitCooldown = 2;

    private float lastDamageTime = Mathf.NegativeInfinity;

    private Transform walkerTransform;
    private WalkerInteractions walkerMovement;


    private void Awake()
    {
        walkerMovement = FindFirstObjectByType<WalkerInteractions>();
        walkerTransform = walkerMovement.transform;
    }

    private void Update()
    {
        Vector3 distanceToWalker = walkerTransform.position - transform.position;

        if (distanceToWalker.magnitude < circleRadius && Time.time - lastDamageTime >= hitCooldown)
        {
            walkerMovement.TakeDamage(damage);
            lastDamageTime = Time.time;
        }

        if (distanceToWalker.magnitude < circleRadius)
            this.transform.forward = Vector3.Normalize(walkerTransform.position - this.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, circleRadius);
    }
}
