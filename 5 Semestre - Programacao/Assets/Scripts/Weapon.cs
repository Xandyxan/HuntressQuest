using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : FirstPersonView
{
    protected LayerMask generalLayer, enemyLayer;

    [SerializeField] protected Transform shootEffectOffSet;
    [SerializeField] protected GameObject shootEffect;

    [SerializeField] protected float ammo, maxAmmoAmount;
    [SerializeField] protected ParticleSystem ammoParticleFeedback;

    protected Image uiFeedbackAmmo;
    protected Animator animator;


    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Shoot() { }

    public virtual void GetAmmo(int ammountAmmo)
    {
        if(this.ammo <= 0) ammoParticleFeedback.Play();
        
        this.ammo += ammountAmmo;
        if (this.ammo > maxAmmoAmount) this.ammo = maxAmmoAmount;

        uiFeedbackAmmo.fillAmount = this.ammo / maxAmmoAmount;
    }
}
