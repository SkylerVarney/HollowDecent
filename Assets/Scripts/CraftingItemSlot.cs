using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CraftingItemSlot : MonoBehaviour, IPointerClickHandler
{

    public string itemName;
    public int quantity;
    public Sprite sprite;
    public Sprite emptySprite;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public GameObject SelectedShader;
    public bool isSelected;

    private CraftingManager craftingManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        craftingManager = GameObject.Find("CraftingCanvas").GetComponent<CraftingManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRigtClick();
        }
    }

    public void OnLeftClick()
    {
        if (!isSelected)
        {
            craftingManager.DeselectAllSlots();
            SelectedShader.SetActive(true);
            isSelected = true;
        }
    }
    
    public void OnRigtClick()
    {
        // Right click functionality can be added here
    }
}
