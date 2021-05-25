using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float totalHealth;
    
    protected virtual void OnTriggerEnter(Collider other)
    {

    }

    public virtual void TakeDamage(float damageValue)
    {
        totalHealth -= damageValue;
        if (totalHealth <= 0)
        {
            totalHealth = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(this.gameObject.name + " Morreu!");
    }


}
