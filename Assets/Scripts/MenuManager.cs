using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    private bool startedGame;

    [SerializeField] AudioSource ambianceSource;
    [SerializeField] AudioSource musicSource;

    private void OnEnable()
    {
        startedGame = false;
        Time.timeScale = 1.0f;
    }

    public void PlayButton()
    {
        startedGame = true;
    }

    private void Update()
    {
        if (canvasGroup == null && startedGame) LoadPlayScene();
        if (!startedGame) return;

        canvasGroup.alpha += Time.deltaTime * 4;

        if(ambianceSource) ambianceSource.volume -= Time.deltaTime * 4;
        if(musicSource) musicSource.volume -= Time.deltaTime * 4;

        if (canvasGroup.alpha >= 1) LoadPlayScene();
    }

    private void LoadPlayScene()
    {
        SceneManager.LoadScene(1);
    }
}
