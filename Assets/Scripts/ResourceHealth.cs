using UnityEngine;

public class ResourceHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Sprite fullSprite;
    public Sprite damagedSprite;
    public Collider2D fullCollider;
    public Collider2D damagedCollider;
    private bool isDamaged;
    private SpriteRenderer sr;

    public GameObject itemDrop;
    public GameObject healthDrop;
    float ringRadius = 2; // how far from the tree’s center
    public int dropCount = 5;
    public float jitter = 0.2f;

    public float regrowTime = 10f;
    float regrowTimer = -1;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (regrowTimer > 0)
        {
            regrowTimer -= Time.deltaTime;
            if (regrowTimer <= 0)
            {
                regrow();
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        if (isDamaged)
            return;

        currentHealth += amount;

        if (currentHealth <= 0) 
            Damaged();
    }

    void Damaged()
    {
        isDamaged = true;
        sr.sprite = damagedSprite;

        SpawnWoodRing();

        // trunkCollider.enabled = false;
        // if (stumpCollider) 
        //     stumpCollider.enabled = true;

        regrowTimer = regrowTime;
    }

    void SpawnWoodRing()
    {
        if (!itemDrop || dropCount <= 0)
            return;

        for (int i = 0; i < dropCount; i++)
        {
            // pick a random direction on the ring
            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector2 pos2D = (Vector2)transform.position + dir * ringRadius;

            // add a little jitter so they’re not perfectly on the circle
            pos2D += Random.insideUnitCircle * jitter;

            Instantiate(itemDrop, pos2D, Quaternion.identity);
        }

        if (healthDrop)
        {
            Vector2 offset = Random.insideUnitCircle * ringRadius;
            Vector2 spawnPos = (Vector2)transform.position + offset;
            Instantiate(healthDrop, spawnPos, Quaternion.identity);
        }
    }
    
    void regrow()
    {
        isDamaged = false;
        sr.sprite = fullSprite;
        currentHealth = maxHealth;

        regrowTimer = -1;
    }
}

