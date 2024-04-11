using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;

    [Header("Inputs")]
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;

    [Header("AttackParameters")]
    [SerializeField] int damagePerHit;
    [SerializeField] float knockBackForce;

    [SerializeField] LayerMask enemyLayer;

    private RaycastHit2D hit;

    private void Update()
    {
        if(Input.GetKeyDown(leftKey))
        {
            Attack(-1);
        }
        if(Input.GetKeyDown(rightKey))
        {
            Attack(1);
        }
    }

    private void Attack(int direction)
    {
        Mathf.Clamp(direction, -1, 1);

        ProjectCollider(direction);

        if(!hit)
        {
            Debug.LogError("Nothing was hit");
            return;
        }
        
        KnockBack(knockBackForce * direction);
        GiveDamage(damagePerHit);
    }

    private void KnockBack(float knockBackForce)
    {
        Rigidbody2D enemyRigidbody = hit.transform.GetComponent<Rigidbody2D>();

        if(enemyRigidbody == null)
        {
            Debug.LogError("Enemy has no 'Rigidbody2D' attached");
            return;
        }

        enemyRigidbody.AddForce(Vector2.right * knockBackForce, ForceMode2D.Impulse);
    }

    private void GiveDamage(int damage)
    {
        HealthSystem enemyHealthSystem = hit.transform.GetComponent<HealthSystem>();

        if (enemyHealthSystem == null)
        {
            Debug.LogError("Enemy has no 'HealthSystem' attached");
            return;
        }

        enemyHealthSystem.TakeDamage(damage);
    }

    private bool ProjectCollider(int direction)
    {
        return hit = Physics2D.Raycast(Vector2.right * direction, Vector2.right * direction, 1, enemyLayer);
    }
}
