using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourBoss : MonoBehaviour
{
    [SerializeField] private Transform playerTransform, offSetBreath;
    [SerializeField] private GameObject _particleSystem, meteor, minion;

    [SerializeField] private GameObject[] targets, targets2;

    private int attackNumber;
    private float timer;
    private bool meteorAttack, minionsAttack;

    public GameObject winScreen;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerOffset").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        meteorAttack = minionsAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        if(timer <= 10)
        {
            FlameBreath();
        }
        else if(timer > 10 && timer <= 20)
        {
            _particleSystem.SetActive(false);
            if(!meteorAttack)StartCoroutine(Meteor());
        }
        else if(timer > 20 && timer <= 30)
        {
            if(!minionsAttack) StartCoroutine(MinionsSpawn());
        }

        if (timer > 30) timer = 0;
        
    }

    private void FlameBreath()
    {
        offSetBreath.LookAt(playerTransform);
        transform.LookAt(playerTransform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        _particleSystem.SetActive(true);
    }

    private IEnumerator Meteor()
    {
        meteorAttack = true;

        for (int i = 0; i < targets.Length; i++)
        {
            Instantiate(meteor, targets[i].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }

        meteorAttack = false;
    }

    private IEnumerator MinionsSpawn()
    {
        minionsAttack = true;

        for (int i = 0; i < targets2.Length; i++)
        {
            Instantiate(minion, targets2[i].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(4f);
        }

        minionsAttack = false;
    }

    public void Die()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
