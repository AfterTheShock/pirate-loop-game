using UnityEngine;

public class SplineManagerSingleton : MonoBehaviour
{
    private static SplineManagerSingleton _instance;

    public static SplineManagerSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<SplineManagerSingleton>();
            }

            return _instance;
        }
    }

    public Transform[] pointsArray = new Transform[0];

    void Awake()
    {
        pointsArray = new Transform[transform.childCount];
        //Get All points from spline
        for (int i = 0; i < transform.childCount; i++)
        {
            pointsArray[i] = transform.GetChild(i).transform;
        }
    }
}
