using UnityEngine;

public class Player : MonoBehaviour
{
    public float detectionRadius = 8f;  // 몬스터 감지 범위
    public LayerMask monsterLayer;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;  // 투사체 발사 위치
    public float attackInterval = 1f;  // 공격 간격
    public Animator animator;  // 애니메이터

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
    // Gizmos를 사용하여 감지 범위를 시각적으로 표시
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // 감지 범위 색상 설정
        Gizmos.DrawWireSphere(transform.position, detectionRadius);  // 감지 범위 표시
    }
}