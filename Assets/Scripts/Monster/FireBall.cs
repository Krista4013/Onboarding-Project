using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 5f; // 파이어볼의 속도
    private Vector2 direction;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }
    }
}