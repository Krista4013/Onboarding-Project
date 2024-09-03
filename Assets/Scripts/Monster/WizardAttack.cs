using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    public float attackDelay = 1f;
    private float lastAttackTime;
    private Animator animator;
    private MonsterMovement monsterMovement;

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

            // ���� ���� (��: ����ü �߻�)
            Debug.Log($"{name}��(��) �÷��̾ ������");
        }
    }
}