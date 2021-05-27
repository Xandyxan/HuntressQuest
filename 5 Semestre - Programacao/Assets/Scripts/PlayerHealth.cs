using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    protected void Start()
    {
        uiHealthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<Image>();
    }

    protected override void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("EnemyDamage"))
        {
            Damage damage = col.GetComponent<Damage>();
            TakeDamage(damage.GetDamageValue());
        }
    }



}
