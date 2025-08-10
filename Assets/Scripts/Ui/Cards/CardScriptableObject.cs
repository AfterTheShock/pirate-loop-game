using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/NewCard")]
public class CardScriptableObject : ScriptableObject
{
    public string cardName;

    public string cardDescription;

    public int cardPrice = 1;

    public int minStock = 1;

    public int maxStock = 2;

    public Sprite cardImage;

    public CardType cardType;

    public PlacementObject placementObject;

    public LocalizedString cardNameLocalized = new LocalizedString();
    public LocalizedString cardDescriptionLocalized = new LocalizedString();
}

public enum CardType
{
    wall,
    flamethrower,
}