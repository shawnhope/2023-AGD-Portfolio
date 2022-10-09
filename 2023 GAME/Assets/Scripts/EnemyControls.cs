using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class EnemyControls : MonoBehaviour
{
    public static Action<GameObject> thisDied;

    [SerializeField] int enemyHealth;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] SpriteRenderer spRen;
    [SerializeField] GameObject player;
    [SerializeField] Animator anim;
    [SerializeField] bool busy;
    private void OnEnable()
    {
        this.agent = this.GetComponent<NavMeshAgent>();
        this.spRen = this.GetComponent<SpriteRenderer>();
        this.anim = this.GetComponent<Animator>();
        enemyHealth = 10;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (!busy) { Decide(); }

        FlipCheck();
        if (agent.remainingDistance > 2.3f) {
            anim.SetFloat("remainingDis",agent.remainingDistance);
        }
    }
    void Decide() {
        busy = true;
        StartCoroutine(WaitToDecide());
    }
    void IsHit() {
        print("been hit");
        if (enemyHealth != 1)
        {
            anim.SetTrigger("staggered");
            enemyHealth--;
        }
        else {
            print("Enemy has died!");
            this.gameObject.SetActive(false);
            thisDied?.Invoke(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IsHit();
    }
    void FlipCheck() {
        if (player.transform.position.x < transform.position.x)
        {
            spRen.flipX = true;
        }
        else {
            spRen.flipX = false;
        }
    }
    void Chase() {
        agent.SetDestination(player.transform.position);
        busy = false;
    }
    public int RandomNum() {
        int num;
        num = UnityEngine.Random.Range(1,10);
        print(num);
        return num;
    }
    public IEnumerator WaitToDecide()
    {
        yield return new WaitForSeconds(2);
        if (RandomNum() >= 5)
        {
            Chase();
        }
        else { busy = false; yield break; }
    }
}
