using UnityEngine;

public class MonsterTakeDamage : MonoBehaviour
{
    public event System.Action OnDeath; // ���Ͱ� ���� �� �߻��ϴ� �̺�Ʈ

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
        OnDeath?.Invoke(); // OnDeath �̺�Ʈ ȣ��
    }
}
