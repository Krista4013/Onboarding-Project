using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public string grade;
    public float speed;
    public int health;
    public event System.Action OnMonsterDeath; // OnMonsterDeath �̺�Ʈ �߰�

    private MonsterAttack attackComponent;
    private MonsterTakeDamage takeDamageComponent;
    private MonsterMovement movementComponent;

    public void Initialize(MonsterDataLoader.MonsterData data)
    {
        monsterName = data.name;
        grade = data.grade;
        speed = data.speed;
        health = data.health;

        // ���� ������Ʈ �ʱ�ȭ
        attackComponent = GetComponent<MonsterAttack>();
        takeDamageComponent = GetComponent<MonsterTakeDamage>();
        movementComponent = GetComponent<MonsterMovement>();

        if (attackComponent != null)
        {
            attackComponent.InitializeAttack();
        }

        if (takeDamageComponent != null)
        {
            takeDamageComponent.InitializeHealth(health);
            takeDamageComponent.OnDeath += Die; // ���� �̺�Ʈ ����
        }

        if (movementComponent != null)
        {
            movementComponent.InitializeMovement(speed);
        }
    }

    private void Die()
    {
        OnMonsterDeath?.Invoke(); // ���Ͱ� ���� �� OnMonsterDeath �̺�Ʈ ȣ��
        Destroy(gameObject); // ���� ������Ʈ ����
    }
}