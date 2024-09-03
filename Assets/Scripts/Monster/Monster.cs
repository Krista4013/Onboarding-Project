using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public string grade;
    public float speed;
    public int health;
    public event System.Action OnMonsterDeath; // OnMonsterDeath 이벤트 추가

    private MonsterAttack attackComponent;
    private MonsterTakeDamage takeDamageComponent;
    private MonsterMovement movementComponent;

    public void Initialize(MonsterDataLoader.MonsterData data)
    {
        monsterName = data.name;
        grade = data.grade;
        speed = data.speed;
        health = data.health;

        // 하위 컴포넌트 초기화
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
            takeDamageComponent.OnDeath += Die; // 죽음 이벤트 연결
        }

        if (movementComponent != null)
        {
            movementComponent.InitializeMovement(speed);
        }
    }

    private void Die()
    {
        OnMonsterDeath?.Invoke(); // 몬스터가 죽을 때 OnMonsterDeath 이벤트 호출
        Destroy(gameObject); // 몬스터 오브젝트 제거
    }
}