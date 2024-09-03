using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed;
    public float detectionRadius = 2f;
    public LayerMask playerLayer;

    private bool isPlayerDetected = false;
    private Transform playerTransform;
    private Animator animator;

    public void InitializeMovement(float speedValue)
    {
        speed = speedValue;

        // Wizard�� ��� ���� ������ ����
        if (GetComponent<WizardAttack>() != null)
        {
            detectionRadius = 6f; // Wizard�� ���� ���� ����
        }
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

        if (hits.Length > 0)
        {
            isPlayerDetected = true;
            playerTransform = hits[0].transform;
            Debug.Log($"�÷��̾�� ������");
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
}