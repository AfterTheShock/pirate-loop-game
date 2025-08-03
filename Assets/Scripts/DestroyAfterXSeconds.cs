using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 3f;
    void Update()
    {
        timeToDestroy -= Time.unscaledDeltaTime;

        if(timeToDestroy <= 0) Destroy(gameObject);
    }
}
