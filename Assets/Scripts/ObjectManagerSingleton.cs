using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerSingleton : MonoBehaviour
{
    [SerializeField] private List<PlacementObject> placementObjects = new List<PlacementObject>();
    private PlacementObject selectedObject;

    private static ObjectManagerSingleton _instance;

    public static ObjectManagerSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<ObjectManagerSingleton>();
            }

            return _instance;
        }
    }
}