using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : Damage
{
    private ParticleSystem _particleSystem;
    [SerializeField] private string tagToDamage;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        if (tagToDamage == "") tagToDamage = "Player";
    }

    private void OnParticleCollision(GameObject col)
    {
        if (tagToDamage == "Player")
        {
            if (col.CompareTag(tagToDamage))
            {
                PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
                if (playerHealth != null) playerHealth.TakeDamage(GetDamageValue());
            }
        }
        else if(tagToDamage == "Enemy")
        {
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth != null) enemyHealth.TakeDamage(GetDamageValue());
        }
    }

}
