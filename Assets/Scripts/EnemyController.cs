using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HealthSystem healthSystem;
    [SerializeField] Animator animator;

    Vector2 directionToplayer;

    [SerializeField] float moveSpeed;
    [SerializeField] float knockBackForce;
    [SerializeField] int damage;

    private void Update()
    {
        MoveToPlayer();

        if(!healthSystem.IsAlive())
            Destroy(gameObject, .2f);
    }

    private void MoveToPlayer()
    {
        animator.SetInteger("AnimState", 2);

        directionToplayer = transform.position;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            directionToplayer = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            if (directionToplayer.x > transform.position.x)
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
            
        transform.Translate(directionToplayer.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    public void KnockBack()
    {
        GetComponent<Rigidbody2D>().AddForce(-directionToplayer.normalized * knockBackForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            KnockBack();

            HealthSystem playerHealthSystem = collision.gameObject.GetComponent<PlayerController>().healthSystem;
            playerHealthSystem.TakeDamage(damage);

            if(!playerHealthSystem.IsAlive())
            {
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }
}
