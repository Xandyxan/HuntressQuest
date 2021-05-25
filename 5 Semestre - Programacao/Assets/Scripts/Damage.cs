using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] protected float damageValue;

    public float GetDamageValue() { return this.damageValue; }
}
