using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float attackDelay = 5f;
    private float lastAttackTime;
    private Animator animator;
    private MonsterMovement monsterMovement;

    public void InitializeAttack(float delay)
    {
        attackDelay = delay;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterMovement = GetComponent<MonsterMovement>();
    }

    void Update()
    {
        if (monsterMovement.IsPlayerDetected())
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackDelay)
        {
            if (animator != null)
            {
                animator.SetTrigger("isAttack");
            }
            lastAttackTime = Time.time;

            // 공격 로직 (예: 투사체 발사)
            Debug.Log($"{name}이(가) 플레이어를 공격함");
        }
    }
}