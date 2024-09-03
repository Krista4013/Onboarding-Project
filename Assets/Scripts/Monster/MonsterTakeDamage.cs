using UnityEngine;

public class MonsterTakeDamage : MonoBehaviour
{
    public int health;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            if (animator != null)
            {
                animator.SetTrigger("isHit");
            }
        }
    }

    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("isDie");
        }
        Destroy(gameObject);
    }
}