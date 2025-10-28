using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    [SerializeField] private AudioClip attackSFX; // drag your attack sound here
    private AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

    }

    public void ChangeHealth(int amount)
    {

        if (attackSFX && audioSource)
        audioSource.PlayOneShot(attackSFX, 3f);

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        else if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
