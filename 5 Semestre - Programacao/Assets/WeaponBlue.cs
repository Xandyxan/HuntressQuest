using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBlue : Weapon
{
    [SerializeField] private GameObject hole;


    protected override void Start()
    {
        maxDistance = 100f;     //override the FirstPersonView
        generalLayer = 1 << 9;
        generalLayer = ~generalLayer;

        enemyLayer = 1 << 10;

        if (ammo <= 0) ammo = 100;
        if (maxAmmoAmount <= 0) maxAmmoAmount = 100;

        uiFeedbackAmmo = GameObject.FindGameObjectWithTag("BlueFeedbackAmmo").GetComponent<Image>();
        uiFeedbackAmmo.fillAmount = this.ammo / 100;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (ammo <= 0) ammoParticleFeedback.Stop();
    }

    protected override void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    protected override void Shoot()
    {
        

        if (ammo >= 2)
        {
            
            Ray raio = playerCamera.ScreenPointToRay(centerPointOfView);
            RaycastHit hit;

            if (Physics.Raycast(raio, out hit, maxDistance, enemyLayer))
            {
                //efeito do tiro na superficie inimiga
                Vector3 position = hit.point + hit.normal / 2;
                Quaternion rotation = Quaternion.LookRotation(-hit.normal);
                GameObject _shootEffect = Instantiate(shootEffect, position, rotation);
                Destroy(_shootEffect, 1f);

                print("atirou no inimigo");
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null) enemyHealth.TakeDamage(1);

                ammo -= 2;

            }
            else if (Physics.Raycast(raio, out hit, maxDistance, generalLayer))
            {
                Vector3 position = hit.point + hit.normal / 100;
                Quaternion rotation = Quaternion.LookRotation(-hit.normal);
                GameObject _hole = Instantiate(hole, position, rotation);
                GameObject _shootEffect = Instantiate(shootEffect, position, rotation);
                Destroy(_hole, 5f);
                Destroy(_shootEffect, 1f);

                ammo -= 2;
            }

            animator.SetTrigger("attackWeapon");
            uiFeedbackAmmo.fillAmount = this.ammo / maxAmmoAmount;
        }

        if (ammo <= 0)
        {
            ammoParticleFeedback.Stop();
        }

    }
}
