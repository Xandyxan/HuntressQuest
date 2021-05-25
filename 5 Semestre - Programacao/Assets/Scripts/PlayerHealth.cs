using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    protected override void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("EnemyDamage"))
        {
            Damage damage = col.GetComponent<Damage>();
            TakeDamage(damage.GetDamageValue());
        }
    }



}
