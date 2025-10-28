using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{

    // -------- Item Data --------
    public string itemName;
    public int quantity;
    public Sprite sprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] private int maxNumberOfItems;

    // -------- Item Slot --------
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    // -------- Item Decritpion Slot --------
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject SelectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start() {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription)
    {
        if (isFull)
            return quantity;

        // set base data
        this.itemName = itemName;
        this.sprite = sprite;
        this.itemDescription = itemDescription;
        itemImage.sprite = sprite;

        // add quantity
        this.quantity += quantity;

        // check max stack
        if (this.quantity >= maxNumberOfItems)
        {
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            isFull = true;
            RefreshUI();
            return extraItems; // leftover
        }

        // update visuals
        isFull = this.quantity >= maxNumberOfItems;
        RefreshUI();
        return 0; // no leftover
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right) {
            OnRigtClick();
        }
    }

    public void OnLeftClick() {
        if (thisItemSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);
            if (usable)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
            }
            
        }

        else
        {
            inventoryManager.DeselectAllSlots();
            SelectedShader.SetActive(true);
            thisItemSelected = true;
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = sprite;
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
    }

    public void OnRigtClick()
    {

    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";
        itemDescriptionImage.sprite = null;
    }
    
    public bool IsEmpty => quantity <= 0 || string.IsNullOrEmpty(itemName);
    public bool Matches(string name) => !IsEmpty && itemName == name;

    public int RemoveUpTo(int amount)
    {
        if (IsEmpty || amount <= 0) return 0;
        int take = Mathf.Min(quantity, amount);
        quantity -= take;

        isFull = quantity >= maxNumberOfItems;

        RefreshUI();

        if (quantity <= 0) Clear();
        return take;
    }

    public void RefreshUI()
    {
        if (quantityText)
        {
            if (quantity > 1)
            {
                quantityText.text = quantity.ToString();
                quantityText.enabled = true;
            }
            else if (quantity == 1)
            {
                quantityText.text = "";
                quantityText.enabled = false;
            }
            else
            {
                quantityText.text = "";
                quantityText.enabled = false;
            }
        }
        if (itemImage && sprite)
            itemImage.sprite = sprite;
    }

    public void Clear()
    {
        itemName = "";
        itemDescription = "";
        sprite = null;
        quantity = 0;
        isFull = false;

        if (itemImage) itemImage.sprite = emptySprite;
        if (quantityText)
        {
            quantityText.text = "";
            quantityText.enabled = false;
        }
    }
}
