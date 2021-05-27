using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour2 : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private Animator animator;
    private CapsuleCollider capsuleCollider;

    [SerializeField] private GameObject areaAttack, triggerSeek;
    private Transform targetToSeek;

    [SerializeField] private GameObject[] meshesToSort;
    private int randomNumber;

    private bool isAlive, isAttacking, isWalking, summoned;
    private bool canAttack;

    [SerializeField] private Transform[] pointsToPatrol;
    private Transform actualPointToGo;
    private bool playerIsClose;

    private EnemyHealth enemyHealth;
    [SerializeField] private ParticleSystem ringParticle, swordsParticle;


    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        targetToSeek = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyHealth = GetComponent<EnemyHealth>();

        isAttacking = isWalking = false;

        actualPointToGo = pointsToPatrol[0].transform;
    }
    private void OnEnable()
    {
        isAlive = true;
        for (int i = 0; i < meshesToSort.Length; i++)
        {
            meshesToSort[i].SetActive(false);
        }
        randomNumber = Random.Range(0, meshesToSort.Length);
        meshesToSort[randomNumber].SetActive(true);

        isAttacking = false;
        isWalking = true;
        canAttack = false;
        summoned = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive)
        {
            if (!playerIsClose && !enemyHealth.GetTookDamge()) Patrol();
            else SeekAndAttack();

            if (enemyHealth.GetTookDamge())
            {
                SetPlayerIsClose(true);
            }
        }
    }

    private void Patrol()
    {
        enemyAgent.SetDestination(actualPointToGo.position);
        if (Mathf.Abs(enemyAgent.remainingDistance) <= Mathf.Abs(enemyAgent.stoppingDistance))
        {
            if (actualPointToGo == pointsToPatrol[0]) actualPointToGo = pointsToPatrol[1];
            else actualPointToGo = pointsToPatrol[0];
        }
        summoned = false;
    }

    private void SeekAndAttack()
    {
        summoned = true;
        enemyAgent.SetDestination(targetToSeek.position);

        if (Mathf.Abs(enemyAgent.remainingDistance) <= Mathf.Abs(enemyAgent.stoppingDistance) && canAttack)
        {
            isAttacking = true;
            isWalking = false;
            transform.LookAt(targetToSeek);
        }
        else if (isWalking)
        {
            isAttacking = false;
            canAttack = true;
        }

        animator.SetBool("attack", isAttacking);
        animator.SetBool("walk", isWalking);

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
        triggerSeek.SetActive(false);

        swordsParticle.Stop();
        ringParticle.Stop();
    }

    public void SetPlayerIsClose(bool _bool)
    {
        this.playerIsClose = _bool;

        if(_bool)
        {
            swordsParticle.Play();
            if (!summoned)
            {
                animator.SetTrigger("summon");
                summoned = true;
                animator.SetBool("attack", false);
            }
        }
        else
        {
            swordsParticle.Stop();
            summoned = false;
            animator.SetBool("attack", false);
        }
    }
}
