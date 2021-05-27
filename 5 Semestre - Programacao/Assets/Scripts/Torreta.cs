using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject projetil;
    public Transform spawn;
    Transform alvo;
    float resfriamento;
    private bool playerIsClose, isAlive;
    private Animator animator;
    public GameObject torre;

    void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("PlayerOffset").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    private void Update()
    {
        if (isAlive)
        {
            if (playerIsClose)
            {
                spawn.LookAt(alvo);
                transform.LookAt(alvo);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

                if (resfriamento < Time.time)
                {
                    resfriamento = Time.time + 0.5f;
                    Vector3 posicao = spawn.position;
                    Quaternion rotacao = Quaternion.FromToRotation(Vector3.up, spawn.forward);
                    GameObject clone = Instantiate(projetil, posicao, rotacao);
                    clone.GetComponent<Rigidbody>().AddForce(spawn.forward * 200);
                    Destroy(clone, 5);
                }
            }
        }
    }

    public void SetPlayerIsClose(bool _bool)
    {
        this.playerIsClose = _bool;
    }

    public void Die()
    {
        isAlive = false;
        animator.enabled = true;
        Destroy(torre, 5f);
    }
}
