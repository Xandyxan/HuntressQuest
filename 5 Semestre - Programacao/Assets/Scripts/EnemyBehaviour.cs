using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    protected NavMeshAgent enemyAgent;
    protected Animator animator;
    protected CapsuleCollider capsuleCollider;
    
    [SerializeField] protected GameObject areaAttack;
    protected Transform targetToSeek;

    [SerializeField] protected GameObject[] meshesToSort;
    protected int randomNumber;

    protected bool isAlive, isAttacking, isWalking;
    protected bool canAttack;

    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        targetToSeek = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        isAttacking = isWalking = false;
    }
    private void OnEnable()
    {
        isAlive = true;
        for(int i = 0; i < meshesToSort.Length; i++)
        {
            meshesToSort[i].SetActive(false);
        }
        randomNumber = Random.Range(0, meshesToSort.Length);
        meshesToSort[randomNumber].SetActive(true);

        isAttacking = false;
        isWalking = true;
        canAttack = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive)
        {
            enemyAgent.SetDestination(targetToSeek.position);

            if (Mathf.Abs(enemyAgent.remainingDistance) <= Mathf.Abs(enemyAgent.stoppingDistance) && canAttack)
            {
                isAttacking = true;
                isWalking = false;
                transform.LookAt(targetToSeek);
            }
            else if(isWalking)
            {
                isAttacking = false;
                canAttack = true;
            }

            animator.SetBool("attack", isAttacking);
            animator.SetBool("walk", isWalking);
        }
    }

    public void TurnAreaAttackOn()
    {
        areaAttack.SetActive(true);
        isWalking = false;
        Invoke("TurnAreAttackOff", 0.25f);
    }

    private void TurnAreAttackOff()
    {
        areaAttack.SetActive(false);
        isWalking = true;
    }

    public void Die()
    {
        isAlive = false;

        animator.SetBool("walk", false);
        animator.SetBool("attack", false);
        animator.SetBool("die", !isAlive);

        enemyAgent.enabled = false;
        capsuleCollider.enabled = false;
        areaAttack.SetActive(false);
    }
}
