using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    public ItemSO[] ItemSOs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory")) {
        
            if (menuActivated)
            {
                Time.timeScale = 1;
                InventoryMenu.SetActive(false);
                menuActivated = false;
                DeselectAllSlots();
            }
            else if (!menuActivated)
            {
                Time.timeScale = 0;
                InventoryMenu.SetActive(true);
                menuActivated = true;
            }
        }
    }

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < ItemSOs.Length; i++)
        {
            if (ItemSOs[i].itemName == itemName)
            {
                bool usable = ItemSOs[i].UseItem();
                return usable;
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {

                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, sprite, itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, sprite, itemDescription);
                }


                    return leftOverItems;
            }
        }
        return quantity;
    }


    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].SelectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
            itemSlot[i].itemDescriptionImage.sprite = null;
            itemSlot[i].ItemDescriptionNameText.text = "";
            itemSlot[i].ItemDescriptionText.text = "";
        }
    }
    
    public int CountByName(string name)
    {
        int total = 0;
        for (int i = 0; i < itemSlot.Length; i++)
        {
            var s = itemSlot[i];
            if (s != null && s.Matches(name))
                total += s.quantity;
        }
        return total;
    }

    public bool RemoveByName(string name, int amount)
    {
        for (int i = 0; i < itemSlot.Length && amount > 0; i++)
        {
            var s = itemSlot[i];
            if (s == null || !s.Matches(name)) continue;
            amount -= s.RemoveUpTo(amount);
        }
        return amount <= 0;
    }
}
