using TMPro;
using UnityEngine;

public class DestroyGOonClick : MonoBehaviour
{
    [SerializeField] GameObject goToTurnOnOnClick;

    SpriteRenderer thisRenderer;

    TextMeshPro textMeshPro;

    private void Start()
    {
        this.TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer);
        this.TryGetComponent<TextMeshPro>(out TextMeshPro text);
        thisRenderer = renderer;
        textMeshPro = text;
    }

    void Update()
    {
        if (thisRenderer != null) 
        { 
            if(Time.deltaTime == 0)
            {
                thisRenderer.enabled = false;
                return;
            }
            else thisRenderer.enabled = true;

        }
        if (textMeshPro != null) 
        { 
            if(Time.deltaTime == 0)
            {
                textMeshPro.enabled = false;
                return;
            }
            else textMeshPro.enabled = true;

        }

        if (Input.GetMouseButtonDown(0)) 
        {
            if(goToTurnOnOnClick != null) goToTurnOnOnClick.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
