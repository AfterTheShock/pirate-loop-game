using UnityEngine;

public class ReverseObject : MonoBehaviour
{
    [SerializeField] private float circleRadius = 0.25f;

    [SerializeField] float timeGoingInReverse = 0.5f;

    private Transform walkerTransform;
    private FollowPointsAndMove walkerMovement;

    private void Awake()
    {
        walkerMovement = FindFirstObjectByType<FollowPointsAndMove>();
        walkerTransform = walkerMovement.transform;
    }


    void Update()
    {
        Vector3 distanceToWalker = walkerTransform.position - transform.position;

        if (distanceToWalker.magnitude < circleRadius)
        {
            walkerMovement.timeGoingInReverse = timeGoingInReverse;

            Destroy(this.gameObject);
        }
    }
}
