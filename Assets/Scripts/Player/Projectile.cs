using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;  // 투사체 속도
    private int damage = 100;  // 투사체 대미지
    private Vector2 direction = Vector2.right;
    public LayerMask monsterLayer;

    private MonsterTakeDamage monsterTakeDamage;


    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void Launch()
    {
        direction = Vector2.right;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayer = collision.gameObject.layer;

        if ((monsterLayer & (1 << collisionLayer)) != 0)
        {
            MonsterTakeDamage monsterTakeDamage = collision.gameObject.GetComponent<MonsterTakeDamage>();
            if (monsterTakeDamage != null)
            {
                monsterTakeDamage.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}