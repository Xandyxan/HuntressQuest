using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRed : Weapon
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float ammoConsumePerSecond;

    // Start is called before the first frame update
    protected override void Start()
    {
        if (ammo <= 0) ammo = 100;
        if (ammoConsumePerSecond <= 0) ammoConsumePerSecond = 15f;
        if (maxAmmoAmount <= 0) maxAmmoAmount = 100;

        uiFeedbackAmmo = GameObject.FindGameObjectWithTag("RedFeedbackAmmo").GetComponent<Image>();
        uiFeedbackAmmo.fillAmount = this.ammo / maxAmmoAmount;
        animator = GetComponent<Animator>();

    }

    private void OnEnable()
    {
        if (ammo <= 0) ammoParticleFeedback.Stop();
    }

    private void OnDisable()
    {
        _particleSystem.Stop();
    }

    protected override void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        else
        {
            _particleSystem.Stop();
            animator.SetBool("attackWeapon", false);
        }
   
    }

    protected override void Shoot()
    {
        if (ammo >= 0)
        {
            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }

            if(_particleSystem.isEmitting)
            {
                ConsumeAmmo(ammoConsumePerSecond);
                animator.SetBool("attackWeapon", true);
            }

        }

        if (ammo <= 0.5f)
        {
            ammo = 0;
            _particleSystem.Stop();
            ammoParticleFeedback.Stop();
            animator.SetBool("attackWeapon", false);
        }
    }

    private void ConsumeAmmo(float consumeAmount)
    {
        ammo -= consumeAmount * Time.deltaTime;
        uiFeedbackAmmo.fillAmount = this.ammo / maxAmmoAmount;
    }
}
