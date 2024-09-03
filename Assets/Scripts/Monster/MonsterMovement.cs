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
        // �÷��̾ �������� �ʾ��� ���� �̵�
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