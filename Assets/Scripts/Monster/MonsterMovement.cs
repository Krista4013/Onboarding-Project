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

        // Wizard의 경우 감지 범위를 조정
        if (GetComponent<WizardAttack>() != null)
        {
            detectionRadius = 6f; // Wizard의 넓은 감지 범위
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
            Debug.Log($"플레이어와 조우함");
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