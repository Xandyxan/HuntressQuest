﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyBehaviour enemyBehaviour;
    private EnemyBehaviour2 enemyBehaviour2;
    private Torreta turret;

    protected void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        enemyBehaviour2 = GetComponent<EnemyBehaviour2>();
        turret = GetComponent<Torreta>();
    }

    protected override void Die()
    {
        Debug.Log(this.gameObject.name + " Morreu!");
        if(enemyBehaviour != null) enemyBehaviour.Die();
        if (enemyBehaviour2 != null) enemyBehaviour2.Die();
        if (turret != null) turret.Die();
    }
}
