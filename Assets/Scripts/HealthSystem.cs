using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] int healthMax;
    [SerializeField] int healthCurrent;

    public int GetHealth()
    {
        return healthCurrent;
    }

    public int GetHealthMax()
    {
        return healthMax;
    }

    public bool IsAlive()
    {
        return healthCurrent > 0;
    }

    public void Revive(float healthPercent)
    {
        Mathf.Clamp(healthPercent, 0, 1);
        healthCurrent = (int)(healthMax * healthPercent);
        animator.SetTrigger("Revive");
    }

    public void TakeDamage(int damage)
    {
        healthCurrent -= damage;

        if(animator != null)
        {
            animator.SetTrigger("Hurt");

            if (!IsAlive())
                animator.SetTrigger("Death");
        }
            
    }
}
