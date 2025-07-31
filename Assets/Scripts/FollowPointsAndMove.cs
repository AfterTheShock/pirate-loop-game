using UnityEngine;

public class FollowPointsAndMove : MonoBehaviour
{
    [Header("General control")]
    private Transform[] pointsArray = new Transform[0];
    [SerializeField] float distanceToChangePoint = 0.005f;

    [Header("Movement")]
    [SerializeField] float currentMovementSpeed = 5f;
    float defaultMovementSpeed = 1f;

    [SerializeField] float currentRotateSpeed = 15f;
    float defaultRotateSpeed = 1f;

    [Header("Data")]
    private int currentIndex = 0;
    public int ammountOfLapsFinished = 0;

    [Header("Stuns")]
    [SerializeField] private float stunnedForXSeconds = 0;
    private bool isStunned = false;

    [Header("Slows")]
    [SerializeField] private float slowedForXSeconds = 0;
    private bool isSlowed = false;
    [SerializeField] float percentageOfSlowSpeedReduction = 0.25f;

    private Rigidbody rigidBody;
    

    void Start()
    {
        //Initialize variables
        defaultMovementSpeed = currentMovementSpeed;
        defaultRotateSpeed = currentRotateSpeed;

        pointsArray = SplineManagerSingleton.Instance.pointsArray;
        
        rigidBody =  GetComponent<Rigidbody>();
    }

    void Update()
    {
        ManageOrderOfPoints();
        MoveAndRotateTowardsCurrentPoint();
        ManageStuns();
        ManageSlows();
    }

    private void ManageSlows()
    {
        if (slowedForXSeconds > 0 && !isStunned && !isSlowed)
        {
            isSlowed = true;
            currentMovementSpeed = defaultMovementSpeed * percentageOfSlowSpeedReduction;
        }
        else if (slowedForXSeconds > 0)
        {
            slowedForXSeconds -= Time.deltaTime;
        }
        else if (isSlowed && slowedForXSeconds <= 0 && !isStunned)
        {
            isSlowed = false;
            slowedForXSeconds = 0;

            currentMovementSpeed = defaultMovementSpeed;
        }
    }

    private void ManageStuns()
    {
        if(stunnedForXSeconds > 0 && !isStunned)
        {
            isStunned = true;
            currentMovementSpeed = 0;
        }
        else if (isStunned && stunnedForXSeconds > 0)
        {
            stunnedForXSeconds -= Time.deltaTime;
        }
        else if(isStunned && stunnedForXSeconds <= 0)
        {
            isStunned = false;
            stunnedForXSeconds = 0;

            currentMovementSpeed = defaultMovementSpeed;
        }
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

    public void KnockbackWalker(float secondsStunned, float forcePower)
    {
        StunWalker(secondsStunned);
        rigidBody.AddForce(-transform.forward * forcePower, ForceMode.Impulse);
    }
    
    public void StunWalker(float secondsStunned)
    {
        stunnedForXSeconds = secondsStunned;
    }
    
    public void SlowWalker(float secondsSlowed)
    {
        stunnedForXSeconds = secondsSlowed;
    }

    private void FinishedCurrentLap()
    {
        ammountOfLapsFinished++;
        // Pasar a la tienda
    }
}
