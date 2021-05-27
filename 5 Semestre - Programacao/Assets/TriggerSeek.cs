using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSeek : MonoBehaviour
{
    private EnemyBehaviour2 enemyBehaviour2;
    private void Awake()
    {
        enemyBehaviour2 = GetComponentInParent<EnemyBehaviour2>();
    }
    private void OnTriggerEnter(Collider col)
    {
        enemyBehaviour2.SetPlayerIsClose(true);
    }

    private void OnTriggerExit(Collider col)
    {
        enemyBehaviour2.SetPlayerIsClose(false);
    }
}
