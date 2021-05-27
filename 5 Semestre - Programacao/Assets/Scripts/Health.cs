using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float totalHealth;
    protected float maxHealth;
    protected Image uiHealthBar;
    protected bool tookDamage;

    protected virtual void Awake()
    {
        maxHealth = totalHealth;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {

    }

    public virtual void TakeDamage(float damageValue)
    {
        totalHealth -= damageValue;
        tookDamage = true;
        Invoke("SetTookDamageFalse", 8f);

        if (totalHealth <= 0)
        {
            totalHealth = 0;
            Die();
        }

        if (uiHealthBar != null) uiHealthBar.fillAmount = this.totalHealth / maxHealth;
    }

    protected virtual void Die()
    {
        Debug.Log(this.gameObject.name + " Morreu!");
    }

    protected virtual void SetTookDamageFalse() { tookDamage = false; }
    public virtual bool GetTookDamge() { return this.tookDamage; }
}
