using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    private int numberSwitch;

    private void Awake()
    {  
        numberSwitch = 0;
        weapons[numberSwitch].SetActive(true);
    }
    private void Start()
    {
        for (int i = 0; i < weapons.Length; i++) weapons[i].SetActive(true);
        for (int i = 0; i < weapons.Length; i++) weapons[i].SetActive(false);

        numberSwitch = 0;
        weapons[numberSwitch].SetActive(true);
    }

    private void Update()
    {
        if ((int)Input.mouseScrollDelta.y > 0)
        {
            numberSwitch++;
            if (numberSwitch >= weapons.Length) numberSwitch = weapons.Length;

            SwitchWeapons();
        }
        
        if ((int)Input.mouseScrollDelta.y < 0)
        {
            numberSwitch--;
            if (numberSwitch <= 1) numberSwitch = 1;
            SwitchWeapons();
        }
    }

    private void SwitchWeapons()
    {
        for (int i = 0; i < weapons.Length; i++) weapons[i].SetActive(false);

        switch (numberSwitch)
        {
            case 1:
                weapons[0].SetActive(true);
                break;

            case 2:
                weapons[1].SetActive(true);
                break;

        }
    }
}
