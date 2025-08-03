using System;
using UnityEngine;

public class WallObject : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] private Transform boxSpawnPoint;
    [SerializeField] private Vector3 boxSize;
    
    [Header("Effects")]
    [SerializeField] private float stunSeconds;
    [SerializeField] private float knockbackPower;
    [SerializeField] private int damage = 5;

    private LayerMask walkerLayerMask;
    private bool alreadyStunned;

    private void Awake()
    {
        walkerLayerMask = LayerMask.GetMask("Walker");
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapBox(boxSpawnPoint.position, boxSize,  transform.rotation, walkerLayerMask);
        if (hits.Length > 0)
        {
            if (!alreadyStunned)
            {
                alreadyStunned = true;
                hits[0].gameObject.GetComponent<FollowPointsAndMove>().KnockbackWalker(transform, stunSeconds, knockbackPower);
                hits[0].gameObject.GetComponent<WalkerInteractions>().TakeDamage(damage);
                DestroyObject();
            }
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxSpawnPoint.position, boxSize * 2);
    }
}
