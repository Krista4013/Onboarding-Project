using UnityEngine;

public class Player : MonoBehaviour
{
    public float detectionRadius = 8f;
    public LayerMask monsterLayer;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float attackInterval = 1f;
    public Animator animator; 

    private float lastAttackTime;

    void Start()
    {
        lastAttackTime = Time.time;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DetectAndAttackMonsters();
    }

    void DetectAndAttackMonsters()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, monsterLayer);

        if (hits.Length > 0)
        {
            animator.SetBool("isIdle", true);

            if (Time.time - lastAttackTime >= attackInterval)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            animator.SetBool("isIdle", false);
        }

    }

    void Attack()
    {
        animator.SetTrigger("isAttack");

        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Projectile projectileComponent = projectile.GetComponent<Projectile>();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}