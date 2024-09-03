using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public string grade;
    public float speed;
    public int health;
    public float detectionRadius = 5f; // �÷��̾� ���� ����
    public LayerMask playerLayer; // �÷��̾� ���̾� ����ũ

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
        // �������� �̵�
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void DetectPlayer()
    {
        // �÷��̾ �ִ��� Ȯ��
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hits.Length > 0)
        {
            isPlayerDetected = true;
            playerTransform = hits[0].transform;
            Debug.Log($"�÷��̾�� ������");
        }
    }

    void AttackPlayer()
    {
        // �÷��̾ �����ϸ� ���߰� ����
        if (playerTransform != null)
        {
            // ���� �ִϸ��̼� ���� ���� �� �� ����
            Debug.Log($"�÷��̾ ������");
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
        OnMonsterDeath?.Invoke();  // �̺�Ʈ
        Destroy(gameObject);  // ����
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
