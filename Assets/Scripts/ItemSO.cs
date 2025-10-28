using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributesToChange attributesToChange = new AttributesToChange();
    public int amountToChangeAttribute;

    public bool UseItem() 
    {
        if (statToChange == StatToChange.health)
        {
            HealthManager playerHealth = GameObject.Find("Player").GetComponent<HealthManager>();
            if (playerHealth.currentHealth == playerHealth.maxHealth)
            {
                return false;
            }
            else
            {
                playerHealth.ChangeHealth(amountToChangeStat);
                return true;
            }
        }
        return false;
    }

    public enum StatToChange
    {
        none,
        health,
        mana,
        stamina
    }

    public enum AttributesToChange 
    {
        none,
        strength,
        defense,
        intelligence,
        agility
    }
}
