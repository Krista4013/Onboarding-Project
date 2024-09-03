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
        Debug.Log($"Layer Mask Value: {playerLayer.value}"); // ��Ʈ����ũ �� ���

        if (hits.Length > 0)
        {
            isPlayerDetected = true;
            playerTransform = hits[0].transform;
            Debug.Log("�÷��̾�� ������");
        }
        else
        {
            Debug.Log("�÷��̾� �������� ����");
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

    // Gizmos�� ����Ͽ� ���� ������ �ð������� ǥ��
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // ���� ���� ���� ����
        Gizmos.DrawWireSphere(transform.position, detectionRadius);  // ���� ���� ǥ��
    }
}