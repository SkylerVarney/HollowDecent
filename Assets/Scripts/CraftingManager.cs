using UnityEngine;
using System;

public class CraftingManager : MonoBehaviour
{

    public GameObject CraftingMenu;
    private bool menuActivated;
    public CraftingItemSlot[] craftingSlots;

    public bool canCraft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canCraft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Crafting"))
        {
            if (canCraft == false)
                return;

            if (menuActivated)
            {
                closeMenu();
            }
            else if (!menuActivated)
            {
                CraftingMenu.SetActive(true);
                menuActivated = true;
            }
        }
    }

    public void closeMenu()
    {
        CraftingMenu.SetActive(false);
        menuActivated = false;
        DeselectAllSlots();
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            craftingSlots[i].SelectedShader.SetActive(false);
            craftingSlots[i].isSelected = false;
        }
    }
}
