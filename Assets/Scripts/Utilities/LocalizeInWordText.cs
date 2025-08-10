using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class LocalizeInWordText : MonoBehaviour
{
    [SerializeField] LocalizedString localizeString = new LocalizedString();
    TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        localizeString.StringChanged += UpdateText;
    }

    private void UpdateText(string s)
    {
        if (!LocalizationUtilities.CheckIfLocalizedStringIsAssinged(localizeString)) return;

        text.text = s;
    }

}
