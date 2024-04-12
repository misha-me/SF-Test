using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HealthSystem healthSystem;
    [SerializeField] Animator animator;

    [Header("Inputs")]
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;

    [Header("AttackParameters")]
    [SerializeField] int damagePerHit;
    [SerializeField] float knockBackForce;
    [SerializeField] float attackDistance;
    [SerializeField] float attackDelay;

    bool attackAvailable = true;

    [SerializeField] LayerMask enemyLayer;

    public int killCount;

    private RaycastHit2D hit;

    private void Update()
    {
        if (healthSystem.IsAlive() && attackAvailable)
        {
            HandleInput();
        }
    }

    public void ResetPlayer()
    {
        healthSystem.Revive(1);
        killCount = 0;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(leftKey))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            animator.SetTrigger("Attack1");
            Attack(-1);

        }
        if (Input.GetKeyDown(rightKey))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            animator.SetTrigger("Attack2");
            Attack(1);
        }
    }

    private void Attack(int direction)
    {
        attackAvailable = false;
        StartCoroutine(AttackCooldown());

        Mathf.Clamp(direction, -1, 1);

        ProjectCollider(direction);

        if(!hit)
        {
            Debug.LogError("Nothing was hit");
            return;
        }
        
        GiveDamage(damagePerHit);
    }

    private void GiveDamage(int damage)
    {
        EnemyController enemycontroller = hit.transform.GetComponent<EnemyController>();

        if (enemycontroller == null)
        {
            Debug.LogError("Enemy has no 'EnemyController' attached");
            return;
        }

        enemycontroller.KnockBack();
        enemycontroller.healthSystem.TakeDamage(damage);
        if(!enemycontroller.healthSystem.IsAlive())
        {
            killCount++;
        }
        
    }

    private bool ProjectCollider(int direction)
    {
        return hit = Physics2D.Raycast(Vector2.zero, Vector2.right * direction, attackDistance, enemyLayer);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackDelay);
        attackAvailable = true;
    }
}
