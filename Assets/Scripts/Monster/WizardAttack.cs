using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    public float attackDelay = 1f;
    private float lastAttackTime;
    private Animator animator;
    private MonsterMovement monsterMovement;
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterMovement = GetComponent<MonsterMovement>();
    }

    void Update()
    {
        if (monsterMovement.IsPlayerDetected())
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackDelay)
        {
            if (animator != null)
            {
                animator.SetTrigger("isAttack");
            }
            lastAttackTime = Time.time;

            // 파이어볼 발사
            ShootFireball();
        }
    }

    void ShootFireball()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

            FireBall fireBallScript = fireball.GetComponent<FireBall>();
            if (fireBallScript != null)
            {
                fireBallScript.SetDirection(Vector2.left);
            }
        }
    }
}