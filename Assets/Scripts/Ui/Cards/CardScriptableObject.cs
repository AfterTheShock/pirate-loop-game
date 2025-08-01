using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/NewCard")]
public class CardScriptableObject : ScriptableObject
{
    public string cardName;

    public int cardPrice = 1;

    public int stock = 1;

    public Sprite cardImage;

    public CardType cardType;

    public PlacementObject placementObject;
}

public enum CardType
{
    wall,
    flamethrower,
}