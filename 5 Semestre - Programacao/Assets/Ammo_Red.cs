using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Red : Ammo
{
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            WeaponRed weaponRed = col.GetComponentInChildren<WeaponRed>();

            if (weaponRed != null)
            {
                weaponRed.GetAmmo(ammoAmount);
                Destroy(this.gameObject);
            }
            
        }
    }
}
