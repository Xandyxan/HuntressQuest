using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public GameObject loseScreen;

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

    protected override void Die()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
