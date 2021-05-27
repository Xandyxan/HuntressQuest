using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTurret : MonoBehaviour
{
    private Torreta torreta;

    private void Awake()
    {
        torreta = GetComponentInChildren<Torreta>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            torreta.SetPlayerIsClose(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            torreta.SetPlayerIsClose(false);
        }
    }
}
