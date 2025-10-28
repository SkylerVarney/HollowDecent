using UnityEngine;

public class CraftingProximity : MonoBehaviour
{
    [SerializeField] private CraftingManager craftingManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crafting Table") && craftingManager)
            craftingManager.canCraft = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Crafting Table") && craftingManager)
        {
            craftingManager.canCraft = false;

            craftingManager.closeMenu();

        }
    }

    void OnDisable()
    {
        if (craftingManager)
            craftingManager.canCraft = false;
    }
}