using UnityEngine;

public class DestroyOnPressSpaceXTimes : MonoBehaviour
{
    [SerializeField] int timesPressedToDestroy = 2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) timesPressedToDestroy--;

        if(timesPressedToDestroy <= 0) Destroy(gameObject);
    }
}
