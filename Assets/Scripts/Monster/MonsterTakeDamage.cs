using UnityEngine;

public class MonsterTakeDamage : MonoBehaviour
{
    public int health;
    public int currenthealth;

    private Animator animator;
    private Monster monsterScript;  // Monster 스크립트 참조

    public event System.Action OnMonsterDeath;

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterScript = GetComponent<Monster>();  // Monster 스크립트 가져오기
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
        monsterScript.HideInfo();  // UI 숨기기
        Destroy(gameObject);
    }
}