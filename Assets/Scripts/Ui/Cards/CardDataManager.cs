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

    private Transform visualsChild;

    private void Awake()
    {
        visualsChild = this.transform.GetChild(0).GetChild(0);

        if (cardScriptableObject == null) return;

        //stock = Random.Range(cardScriptableObject.minStock, cardScriptableObject.maxStock + 1);

        SetCardVisuals();
    }

    private void OnDestroy()
    {
        Destroy(visualsChild.gameObject);
    }

    private void Start()
    {
        stock = Random.Range(cardScriptableObject.minStock, cardScriptableObject.maxStock + 1);

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

        //Substract the money from the player's money
        ShopManager.Instance.SubstractMoney(cardScriptableObject.cardPrice);

        //Give the card to the players hand
        ShopManager.Instance.CardBought(cardScriptableObject);

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

    public void PlaceCard()
    {
        ObjectPlacerSingleton.Instance.CardToPlace(cardScriptableObject);
        Destroy(this.gameObject);
    } 

    public void OnHoverOverCard()
    {
        CardFlotatingDescriptionManager.Instance.descriptionText.text = cardScriptableObject.cardDescription;
        CardFlotatingDescriptionManager.Instance.thisChild.gameObject.SetActive(true);
        CardFlotatingDescriptionManager.Instance.thisCanvasGroup.alpha = 0;
    }

    public void OnHoverOutsideCard()
    {
        CardFlotatingDescriptionManager.Instance.thisChild.gameObject.SetActive(false);
    }
}
