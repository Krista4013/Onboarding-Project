using System;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed;
    public float detectionRadius;
    public LayerMask playerLayer;

    private bool isPlayerDetected = false;
    private Transform playerTransform;
    private Animator animator;

    public void InitializeMovement(float speedValue)
    {
        speed = speedValue;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isPlayerDetected)
        {
            MoveLeft();
            DetectPlayer();
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (animator != null)
        {
            animator.SetBool("isWalk", true);
        }
    }

    void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        Debug.Log($"Detection Radius: {detectionRadius}");
        Debug.Log($"Layer Mask Value: {playerLayer.value}"); // 비트마스크 값 출력

        if (hits.Length > 0)
        {
            isPlayerDetected = true;
            playerTransform = hits[0].transform;
            Debug.Log("플레이어와 조우함");
        }
        else
        {
            Debug.Log("플레이어 감지되지 않음");
        }
    }

    public bool IsPlayerDetected()
    {
        return isPlayerDetected;
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    // Gizmos를 사용하여 감지 범위를 시각적으로 표시
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // 감지 범위 색상 설정
        Gizmos.DrawWireSphere(transform.position, detectionRadius);  // 감지 범위 표시
    }
}