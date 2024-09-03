using UnityEngine;

public class MonsterTakeDamage : MonoBehaviour
{
    public event System.Action OnDeath; // 몬스터가 죽을 때 발생하는 이벤트

    private int currentHealth;

    public void InitializeHealth(int health)
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(); // OnDeath 이벤트 호출
    }
}
