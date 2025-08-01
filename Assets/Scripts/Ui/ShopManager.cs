using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int  currentAmmountOfMoney = 0;

    public RectTransform cardInitialPos;

    private static ShopManager _instance;
    public static ShopManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<ShopManager>();
            }

            return _instance;
        }
    }

    [SerializeField] CardScriptableObject[] posibleCards = new CardScriptableObject[0];

    [SerializeField] GameObject cardsHolder;

    [SerializeField] GameObject visualsShopHolder;

    [SerializeField] CanvasGroup visualsShopCanvasGroup;

    [SerializeField] GameObject initialCardPosition;

    [SerializeField] GameObject visualsInitialPosition;

    [SerializeField] GameObject cardPrefab;

    [Header("Cards in hand")]
    [SerializeField] Transform cardsInCandHolder;
    [SerializeField] GameObject handCardPrefab;
    [SerializeField] GameObject handCardStartPosition;

    private void Start()
    {
        HudManager.Instance.SetMoneyText(currentAmmountOfMoney);
        ExitShopButton();
    }

    private void Update()
    {
        if (visualsShopHolder.activeInHierarchy)
        {
            visualsShopCanvasGroup.alpha += Time.deltaTime * 6;
        }

        //SACAR ESTO ANDES DE BUILDEAR ES PARA TESTEAR
        if (Input.GetKeyDown(KeyCode.P)) EnterShop();
    }

    public void SubstractMoney(int ammountToPay)
    {
        currentAmmountOfMoney -= ammountToPay;

        HudManager.Instance.SetMoneyText(currentAmmountOfMoney);
    }

    public void AddMoney(int ammountToGive)
    {
        currentAmmountOfMoney += ammountToGive;

        HudManager.Instance.SetMoneyText(currentAmmountOfMoney);
    }

    public void CardBought(CardScriptableObject cardScriptableObject)
    {
        GameObject card = Instantiate(handCardPrefab);

        card.transform.SetParent(cardsInCandHolder);
        card.transform.localScale = Vector3.one;
        card.transform.localPosition = handCardStartPosition.transform.position;
        card.transform.GetChild(0).GetChild(0).localPosition = Vector3.zero;
        card.transform.rotation = handCardStartPosition.transform.rotation;

        card.GetComponent<CardDataManager>().cardScriptableObject = cardScriptableObject;
    }

    public void EnterShop()
    {
        ExitShopButton();

        for(int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(cardPrefab);
            card.transform.SetParent(cardsHolder.transform);
            card.transform.localPosition = initialCardPosition.transform.position;
            card.transform.GetChild(0).GetChild(0).localPosition = visualsInitialPosition.transform.position;
            card.transform.localScale = Vector3.one;
            card.transform.rotation = cardsHolder.transform.rotation;

            card.GetComponent<CardDataManager>().cardScriptableObject = posibleCards[Random.Range(0, posibleCards.Length)];
        }

        visualsShopHolder.SetActive(true);
    }

    public void ExitShopButton()
    {
        visualsShopCanvasGroup.alpha = 0;
        visualsShopHolder.SetActive(false);

        foreach (Transform child in cardsHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }


}
