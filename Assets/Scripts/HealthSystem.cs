using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
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

    public void TakeDamage(int damage)
    {
        healthCurrent -= damage;
    }
}
