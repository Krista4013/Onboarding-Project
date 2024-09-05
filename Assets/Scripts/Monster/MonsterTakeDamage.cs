using UnityEngine;

public class MonsterTakeDamage : MonoBehaviour
{
    public int health;
    public int currenthealth;

    private Animator animator;
    private Monster monsterScript;  // Monster ��ũ��Ʈ ����

    public event System.Action OnMonsterDeath;

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterScript = GetComponent<Monster>();  // Monster ��ũ��Ʈ ��������
        currenthealth = health;
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        animator.SetTrigger("isHit");
        if (currenthealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("isDie");
        OnMonsterDeath?.Invoke();
        monsterScript.HideInfo();  // UI �����
        Destroy(gameObject);
    }
}