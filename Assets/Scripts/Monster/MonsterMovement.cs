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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        if (hits.Length > 0)
        {
            isPlayerDetected = true;
            playerTransform = hits[0].transform;
            animator.SetBool("isWalk", false);
        }
        else
        {
            isPlayerDetected = false;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}