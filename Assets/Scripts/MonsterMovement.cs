using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private float moveSpeed;

    public void InitializeMovement(float speed)
    {
        moveSpeed = speed;
    }

    void Update()
    {
        // 플레이어를 감지하지 않았을 때만 이동
        if (!GetComponent<MonsterAttack>().isPlayerDetected)
        {
            MoveLeft();
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}