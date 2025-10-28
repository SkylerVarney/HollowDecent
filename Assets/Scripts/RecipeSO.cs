using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ScriptableObjects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    [System.Serializable]
    public struct Ingredient
    {
        public ItemSO item;
        public int quantity;
    }

    public List<Ingredient> ingredients = new();

    public enum EffectType
    {
        none,
        increaseHealth,
        increaseMana,
        increaseStamina,
        increaseAttack
    }

    public EffectType effect = EffectType.increaseAttack;
    public int effectAmount;
    public bool temporyEffect;
    public float effectDuration;
}
