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
        // 플레이어를 공격하는 로직
        Debug.Log($"{name}이(가) 플레이어를 공격함");
    }
}
