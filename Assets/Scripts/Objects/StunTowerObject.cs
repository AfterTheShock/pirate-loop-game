using UnityEngine;

public class StunTowerObject : MonoBehaviour
{
    [Header("Stun Area")]
    [SerializeField] private float circleRadius;
    [SerializeField] private int damage;
    
    [Header("Effects")]
    [SerializeField] private float stunSeconds;
    [SerializeField] private float stunCooldown;
    
    private float lastDamageTime = Mathf.NegativeInfinity;
    
    private Transform walkerTransform;
    private FollowPointsAndMove walkerMovement;
    private WalkerInteractions walkerInteractions;
    
    [SerializeField] Animator animator;

    [SerializeField] AudioSource shootAudioSource;

    private void Awake()
    {
        walkerMovement = FindFirstObjectByType<FollowPointsAndMove>();
        walkerTransform = walkerMovement.transform;
        walkerInteractions = walkerTransform.GetComponent<WalkerInteractions>();
    }

    private void Update()
    {
        Vector3 distanceToWalker = walkerTransform.position - transform.position;

        if (distanceToWalker.magnitude < circleRadius && Time.time - lastDamageTime >= stunCooldown)
        {
            walkerMovement.StunWalker(stunSeconds);
            lastDamageTime = Time.time;
            walkerInteractions.TakeDamage(damage);
            animator.Play("ShootCatapult");
            if (shootAudioSource != null) shootAudioSource.Play();
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
