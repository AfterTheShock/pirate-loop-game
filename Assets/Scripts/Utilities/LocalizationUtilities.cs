using UnityEngine;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;

public static class LocalizationUtilities
{
    public static bool CheckIfLocalizedStringIsAssinged(LocalizedString myString)
    {
        return myString.TableReference.ReferenceType != TableReference.Type.Empty &&
            myString.TableEntryReference.ReferenceType != TableEntryReference.Type.Empty;
    }
}
