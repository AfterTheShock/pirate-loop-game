using System;
using UnityEngine;

public class SlowerObject : MonoBehaviour
{
    [Header("Flame Area")]
    [SerializeField] private float circleRadius;
    
    [Header("Effects")]
    [SerializeField] private float slowSeconds;
    [SerializeField] private float slowPercent;
    
    private Transform walkerTransform;
    private FollowPointsAndMove walkerMovement;

    private void Awake()
    {
        walkerMovement = FindFirstObjectByType<FollowPointsAndMove>();
        walkerTransform = walkerMovement.transform;
    }

    private void Update()
    {
        Vector3 distanceToWalker = walkerTransform.position - transform.position;

        if (distanceToWalker.magnitude < circleRadius)
        {
            walkerMovement.SlowWalker(slowSeconds, slowPercent);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, circleRadius);
    }
}