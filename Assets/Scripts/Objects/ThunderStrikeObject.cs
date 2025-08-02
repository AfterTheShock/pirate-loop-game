using System;
using UnityEngine;

public class ThunderStrikeObject : MonoBehaviour
{
    [Header("Thunder Area")]
    [SerializeField] private float circleRadius;
    
    [Header("Effects")]
    [SerializeField] private float stunSeconds;
    
    private Transform walkerTransform;
    private FollowPointsAndMove walkerMovement;

    private void Awake()
    {
        walkerMovement = FindFirstObjectByType<FollowPointsAndMove>();
        walkerTransform = walkerMovement.transform;
    }

    private void Start()
    {
        Vector3 distanceToWalker = walkerTransform.position - transform.position;

        if (distanceToWalker.magnitude < circleRadius)
        {
            walkerMovement.StunWalker(stunSeconds);
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, circleRadius);
    }
}
