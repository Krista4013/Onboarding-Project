using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public string grade;
    public float speed;
    public int health;

    public event System.Action OnMonsterDeath;

    public void Initialize(MonsterDataLoader.MonsterData data)
    {
        monsterName = data.name;
        grade = data.grade;
        speed = data.speed;
        health = data.health;
    }

    public void Die()
    {
        OnMonsterDeath?.Invoke();
        Destroy(gameObject);
    }
}