using System;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    [SerializeField] private Transform boxSpawnPoint;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private LayerMask walkerMask;
    [SerializeField] private float stunSeconds;
    [SerializeField] private float knockbackPower;
 
    private PreviewObjectCheck previewObjectCheck;

    private bool alreadyStunned = false;

    private void Update()
    {
        Collider[] hits = Physics.OverlapBox(boxSpawnPoint.position, boxSize,  transform.rotation, walkerMask);
        if (hits.Length > 0)
        {
            if (!alreadyStunned)
            {
                alreadyStunned = true;
                hits[0].gameObject.GetComponent<FollowPointsAndMove>().KnockbackWalker(stunSeconds, knockbackPower);
                DestroyObject();
            }
        }
    }

    private void DestroyObject()
    {
        // Aca pueden ir animaciones y efectos de sonido previos a destruir el objeto
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxSpawnPoint.position, boxSize * 2);
    }
}
