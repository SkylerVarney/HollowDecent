using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public SpriteRenderer spriteRenderer;
    public Movement playerMovement;

    private Vector3 respawnPosition = Vector3.zero;

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth <= 0)
        {
            transform.position = respawnPosition;
            currentHealth = 3;

            //spriteRenderer.enabled = false;
            //playerMovement.enabled = false;
        }
    }
    
    
}
