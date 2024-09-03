using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float detectionRadius = 5f;
    public LayerMask playerLayer;

    private Transform playerTransform;
    public bool isPlayerDetected = false;

    public void InitializeAttack()
    {
    }

    void Update()
    {
        if (!isPlayerDetected)
        {
            DetectPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hits.Length > 0)
        {
            isPlayerDetected = true;
            playerTransform = hits[0].transform;
        }
    }

    void AttackPlayer()
    {
        // �÷��̾ �����ϴ� ����
        Debug.Log($"{name}��(��) �÷��̾ ������");
    }
}
