using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public string grade;
    public float speed;
    public int health;
    public float detectionRadius = 5f; // 플레이어 감지 범위
    public LayerMask playerLayer; // 플레이어 레이어 마스크

    private bool isPlayerDetected = false;
    private Transform playerTransform;
    private Animator animator;

    public event System.Action OnMonsterDeath;

    public void Initialize(MonsterDataLoader.MonsterData data)
    {
        monsterName = data.name;
        grade = data.grade;
        speed = data.speed;
        health = data.health;
    }

    void Update()
    {
        if (!isPlayerDetected)
        {
            MoveLeft();
            DetectPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    void MoveLeft()
    {
        // 왼쪽으로 이동
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void DetectPlayer()
    {
        // 플레이어가 있는지 확인
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hits.Length > 0)
        {
            isPlayerDetected = true;
            playerTransform = hits[0].transform;
            Debug.Log($"플레이어와 조우함");
        }
    }

    void AttackPlayer()
    {
        // 플레이어를 감지하면 멈추고 공격
        if (playerTransform != null)
        {
            // 공격 애니메이션 실행 등을 할 수 있음
            Debug.Log($"플레이어를 공격함");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnMonsterDeath?.Invoke();  // 이벤트
        Destroy(gameObject);  // 제거
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
