using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDataManager : MonoBehaviour
{
    public CardScriptableObject cardScriptableObject;

    [SerializeField] int stock = 1;

    [SerializeField] TextMeshProUGUI cardNameText;

    [SerializeField] TextMeshProUGUI cardPriceText;

    [SerializeField] TextMeshProUGUI stockText;

    [SerializeField] Image cardImage;


    private void Awake()
    {
        if (cardScriptableObject == null) return;

        stock = cardScriptableObject.stock;

        SetCardVisuals();
    }

    private void Start()
    {
        stock = cardScriptableObject.stock;

        SetCardVisuals();
    }

    public void ChooseCardToBuy()
    {
        if (cardScriptableObject == null) return;

        //Check if player has enough money
        if (ShopManager.Instance.currentAmmountOfMoney < cardScriptableObject.cardPrice) 
        {
            //Not enough money
            return;
        } 

        ShopManager.Instance.SubstractMoney(cardScriptableObject.cardPrice);

        //dar la carta al maso

        stock--;
        SetCardVisuals();

        if (stock <= 0) Destroy(this.gameObject);
    }

    private void SetCardVisuals()
    {
        if (cardNameText != null) cardNameText.text = cardScriptableObject.cardName;

        if (cardPriceText != null) cardPriceText.text = cardScriptableObject.cardPrice.ToString() + "$";

        if (stockText != null) stockText.text = "Stock: " + stock.ToString();

        if (cardImage != null) cardImage.sprite = cardScriptableObject.cardImage;
    }
}
