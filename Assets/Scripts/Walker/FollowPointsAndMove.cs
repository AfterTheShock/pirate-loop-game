using System.Collections.Generic;
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
    private float percentageOfSlowSpeedReduction;
    
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
        // Cambio estado isSlowed
        if (slowedForXSeconds > 0 && !isSlowed)
        {
            isSlowed = true;
            currentMovementSpeed = defaultMovementSpeed - (defaultMovementSpeed * percentageOfSlowSpeedReduction);
        }
        // Cuenta regresiva de slow
        else if (slowedForXSeconds > 0)
        {
            slowedForXSeconds -= Time.deltaTime;
        }
        // El walker no está sloweado
        else if (isSlowed && slowedForXSeconds <= 0)
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
        }
        else if (isStunned && stunnedForXSeconds > 0)
        {
            stunnedForXSeconds -= Time.deltaTime;
        }
        else if(isStunned && stunnedForXSeconds <= 0)
        {
            isStunned = false;
            stunnedForXSeconds = 0;
            percentageOfSlowSpeedReduction = 0;
        }
    }

    private void MoveAndRotateTowardsCurrentPoint()
    {
        if(isStunned) return;
        
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
        isSlowed = false;
        stunnedForXSeconds = secondsStunned;
    }
    
    public void SlowWalker(float secondsSlowed, float slowPercent)
    {
        if (slowPercent < percentageOfSlowSpeedReduction) return;

        percentageOfSlowSpeedReduction = slowPercent;
        slowedForXSeconds = secondsSlowed;
    }

    private void FinishedCurrentLap()
    {
        ammountOfLapsFinished++;
        // Pasar a la tienda
        FinishedLapResultsManager.Instance.OpenResultsScreen();
    }
}
