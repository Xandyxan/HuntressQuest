using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Blue : Ammo
{
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            WeaponBlue weaponBlue = col.GetComponentInChildren<WeaponBlue>();

            if (weaponBlue != null)
            {
                weaponBlue.GetAmmo(ammoAmount);
                Destroy(this.gameObject);
            }


        }
    }
}
