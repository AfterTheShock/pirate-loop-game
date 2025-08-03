using UnityEngine;

public class GameStartBlackScreenManager : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    [SerializeField] float timeToStartAlphaChange = 0.1f;
    private float currentTime;

    private void Awake()
    {
        if(canvasGroup != null) canvasGroup.alpha = 1.0f;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime < timeToStartAlphaChange) return;

        if (canvasGroup == null) return;

        canvasGroup.alpha -= Time.deltaTime * 1;

        if (canvasGroup.alpha <= 0) Destroy(this.gameObject);
    }
}
