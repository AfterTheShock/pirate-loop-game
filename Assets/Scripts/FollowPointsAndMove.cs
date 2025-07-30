using UnityEngine;

public class FollowPointsAndMove : MonoBehaviour
{
    [SerializeField] Transform[] pointsArray = new Transform[0];

    [SerializeField] float distanceToChangePoint = 0.005f;

    [SerializeField] float currentMovementSpeed = 5f;

    float deffaultMovementSpeed = 1f;

    [SerializeField] float currentRotateSpeed = 15f;

    float deffaultRotateSpeed = 1f;

    private int currentIndex = 0;

    public int ammountOfLapsFinished = 0;

    void Start()
    {
        //Initialize variables
        deffaultMovementSpeed = currentMovementSpeed;
        deffaultRotateSpeed = currentRotateSpeed;

        pointsArray = SplineManagerSingleton.Instance.pointsArray;
    }

    void Update()
    {
        ManageOrderOfPoints();
        MoveAndRotateTowardsCurrentPoint();
    }

    private void MoveAndRotateTowardsCurrentPoint()
    {
        //Move 
        Vector3 movementDirection = Vector3.Normalize(pointsArray[currentIndex].position - this.transform.position);
        this.transform.position += movementDirection * currentMovementSpeed * Time.deltaTime;

        //Rotate
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection, this.transform.up);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, currentRotateSpeed * Time.deltaTime);
    }

    //Check if shoud change point and do it
    private void ManageOrderOfPoints()
    {
        float distanceToCurrentPoint = Vector3.Distance(transform.position, pointsArray[currentIndex].position);

        if(distanceToCurrentPoint <= distanceToChangePoint) //Change point
        {
            if (pointsArray.Length <= currentIndex + 1)
            {
                //If this is true finished the current lap and start over
                currentIndex = 0;
                FinishedCurrentLap();
            }
            else
            {
                //Go to next point
                currentIndex++;
            }
        }
    }

    private void FinishedCurrentLap()
    {
        ammountOfLapsFinished++;

    }
}
