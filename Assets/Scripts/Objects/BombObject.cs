using System.Collections;
using UnityEngine;

public class BombObject : MonoBehaviour
{
    [SerializeField] private float circleRadius;
    
    [SerializeField] ParticleSystem particles;
    [SerializeField] private GameObject model;
    
    private void Start()
    {
        GameObject[] buildingsToDestroy = GameObject.FindGameObjectsWithTag("Building");
        
        foreach (GameObject obj in buildingsToDestroy)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist <= circleRadius)
                DestroyBuilding(obj);
        }
        
        particles.Play();
        model.SetActive(false);
        StartCoroutine(WaitForDestroy());
    }

    private IEnumerator WaitForDestroy()
    {
        while (particles.IsAlive())
        {
            yield return null;
        }
        
        Destroy(gameObject);
    }
    
    private void DestroyBuilding(GameObject building)
    {
        Destroy(building);
    }
}