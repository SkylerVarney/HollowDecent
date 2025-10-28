using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Transform attackPoint;
    public float weaponRange = 1;
    public float toolRange = 1;
    public LayerMask enemyLayer;
    public LayerMask resourceLayer;
    public int attackDamage = 1;

    public Animator animator;

    public float cooldown = 0;
    private float timer;

    [SerializeField] private AudioClip attackSFX; // drag your attack sound here
    private AudioSource audioSource;              // audio player

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            animator.SetBool("IsAttacking", true);

            if (attackSFX && audioSource)
            audioSource.PlayOneShot(attackSFX, 1f);

            timer = cooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                 enemies[i].GetComponent<EnemyHealth>().ChangeHealth(-attackDamage); //FIX LATER
            }
        }
            
    }
    
    public void DamageResource()
    {
        Collider2D[] resource = Physics2D.OverlapCircleAll(attackPoint.position, toolRange, resourceLayer);
        
        if(resource.Length > 0)
        {
            for (int i = 0; i < resource.Length; i++)
            {
                resource[i].GetComponent<ResourceHealth>().ChangeHealth(-attackDamage);
            }
        }
    }

    public void StopAttack()
    {
        animator.SetBool("IsAttacking", false);
    }
}
