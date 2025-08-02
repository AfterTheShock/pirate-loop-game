using System;
using UnityEngine;

public class FlamethrowerObject : MonoBehaviour
{
    [Header("Flame Area")]
    [SerializeField] private float coneAngle;
    [SerializeField] private float coneRange;

    [Header("Effects")]
    [SerializeField] private float slowSeconds;
    [SerializeField] private float slowPercent;
    [SerializeField] private float damageCooldown;
    [SerializeField] private int damagePerTick = 1;
    
    private float lastDamageTime = Mathf.NegativeInfinity;
    
    private Transform walkerTransform;
    private FollowPointsAndMove walkerMovement;
    private WalkerInteractions walkerInteractions;

    private void Awake()
    {
        walkerMovement = FindFirstObjectByType<FollowPointsAndMove>();
        walkerTransform = walkerMovement.transform;
        walkerInteractions = walkerTransform.GetComponent<WalkerInteractions>();
    }

    private void Update()
    {
        
        bool isInsideFlames = IsInsideCone(transform.position, transform.forward, walkerTransform.position, coneAngle, coneRange);
        
        if (isInsideFlames && Time.time - lastDamageTime >= damageCooldown)
        {
            if(slowPercent != 0) walkerMovement.SlowWalker(slowSeconds, slowPercent);
            walkerInteractions.TakeDamage(damagePerTick);
            lastDamageTime = Time.time;
        }
    }
    
    private bool IsInsideCone(Vector3 origin, Vector3 direction, Vector3 point, float angle, float range)
    {
        Vector3 toPoint = point - origin;
        if (toPoint.magnitude > range) return false;

        float dot = Vector3.Dot(direction.normalized, toPoint.normalized);
        float angleToPoint = Mathf.Acos(dot) * Mathf.Rad2Deg;

        return angleToPoint <= angle / 2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, coneAngle / 2, 0) * transform.forward * coneRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -coneAngle / 2, 0) * transform.forward * coneRange);
    }
}
