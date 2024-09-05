using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;  // ����ü �ӵ�
    private int damage = 100;  // ����ü �����
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
        // �浹�� ������Ʈ�� ���̾� Ȯ��
        int collisionLayer = collision.gameObject.layer;

        if ((monsterLayer & (1 << collisionLayer)) != 0)
        {
            MonsterTakeDamage monsterTakeDamage = collision.gameObject.GetComponent<MonsterTakeDamage>();
            if (monsterTakeDamage != null)
            {
                monsterTakeDamage.TakeDamage(damage);  // ���Ϳ��� ����� ����
            }
            Destroy(gameObject);  // ����ü ����
        }
    }
}