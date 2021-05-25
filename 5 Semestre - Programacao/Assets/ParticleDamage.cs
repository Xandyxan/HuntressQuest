using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : Damage
{
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject col)
    {
        if(col.CompareTag("Player"))
        {
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(GetDamageValue());
        }
    }

}
