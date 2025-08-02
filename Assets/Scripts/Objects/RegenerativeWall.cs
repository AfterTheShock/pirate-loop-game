using UnityEngine;

public class RegenerativeWall : MonoBehaviour
{
    [Header("Regeneration")]
    [SerializeField] float timeToRegenerate = 7;
    private float timeSinceBroken = 0;
    [SerializeField] GameObject functioningModel;
    [SerializeField] GameObject brokenModel;
    private bool isBroken;

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

        if (isBroken) DestroyObject();
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapBox(boxSpawnPoint.position, boxSize, transform.rotation, walkerLayerMask);
        if (hits.Length > 0)
        {
            if (!alreadyStunned && !isBroken)
            {
                alreadyStunned = true;
                hits[0].gameObject.GetComponent<FollowPointsAndMove>().KnockbackWalker(stunSeconds, knockbackPower);
                hits[0].gameObject.GetComponent<WalkerInteractions>().TakeDamage(damage);
                DestroyObject();
            }
        }

        ManageRegeneration();
    }

    private void ManageRegeneration()
    {
        if (isBroken)
        {
            timeSinceBroken += Time.deltaTime;

            if(timeToRegenerate <= timeSinceBroken)
            {
                FixObject();
            }
        }
    }

    private void DestroyObject()
    {
        functioningModel.SetActive(false);
        brokenModel.SetActive(true);
        isBroken = true;
    }

    private void FixObject()
    {
        functioningModel.SetActive(true);
        brokenModel.SetActive(false);
        alreadyStunned = false;
        isBroken = false;
        timeSinceBroken = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxSpawnPoint.position, boxSize * 2);
    }
}
