using UnityEngine;

public class FollowPointsAndMove : MonoBehaviour
{
    [SerializeField] Transform[] pointsArray = new Transform[0];

    [SerializeField] float distanceToChangePoint = 0.005f;

    [SerializeField] float movementSpeed = 1f;

    [SerializeField] float rotateSpeed = 1f;

    private int currentIndex = 0;

    void Update()
    {
        ManageOrderOfPoints();
        MoveAndRotateTowardsCurrentPoint();
    }

    private void MoveAndRotateTowardsCurrentPoint()
    {
        //Move 
        Vector3 movementDirection = Vector3.Normalize(pointsArray[currentIndex].position - this.transform.position);
        this.transform.position += movementDirection * movementSpeed * Time.deltaTime;

        //Rotate
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection, this.transform.up);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
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
            }
            else
            {
                //Go to next point
                currentIndex++;
            }
        }
    }
}
