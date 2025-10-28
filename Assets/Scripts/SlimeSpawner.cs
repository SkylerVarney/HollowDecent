using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject slimePrefab;   // drag your slime prefab here
    public float spawnInterval = 5f; // time between spawns
    public float spawnRadius = 10f;  // how close player must be to start spawning

    private Transform player;
    private float timer;
    private bool playerInRange;      // track when player enters/leaves range

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.position, transform.position);

        // player just entered range
        if (!playerInRange && distance <= spawnRadius)
        {
            playerInRange = true;
            timer = 0f;          // reset timer
            SpawnSlime();        // spawn immediately!
        }
        // player left range
        else if (playerInRange && distance > spawnRadius)
        {
            playerInRange = false;
        }

        // if in range, spawn on interval
        if (playerInRange)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnSlime();
            }
        }
    }

    void SpawnSlime()
    {
        Instantiate(slimePrefab, transform.position, Quaternion.identity);
    }
}