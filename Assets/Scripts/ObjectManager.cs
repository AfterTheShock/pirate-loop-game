using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] List<PlacementObject> placementObjects = new List<PlacementObject>();
    private PlacementObject selectedObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedObject = placementObjects[0];
            ObjectPlacerSingleton.Instance.SetObjectToPlace(selectedObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedObject = placementObjects[1];
            ObjectPlacerSingleton.Instance.SetObjectToPlace(selectedObject);
        }
    }
}