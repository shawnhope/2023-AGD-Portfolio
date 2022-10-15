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
    [SerializeField] Vector3 destination;
    [SerializeField] bool busy,engaged;

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
        if (agent.remainingDistance > 2.3f)
        {
            anim.SetFloat("remainingDis", agent.remainingDistance);
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
            agent.ResetPath();
            //agent.isStopped=true;
            //agent.isStopped = false;
            engaged = true;
        }
        else {
            print("Enemy has died!"); 
            engaged = false;

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
    void Wander() { 
        destination = UnityEngine.Random.insideUnitSphere * 10 + player.transform.position;
        agent.SetDestination(destination);
        busy = false;  
    }
    public int RandomNum(int n, int m) {
        int num;
        num = UnityEngine.Random.Range(n,m);
        print(num);
        return num;
    }
    public IEnumerator WaitToDecide()
    {
        yield return new WaitForSeconds(2);
        if (RandomNum(1, 11) >= 4)
        {
            if (!engaged) { Wander(); }
            else { Chase(); }
        }
        else { busy = false; yield break; }
    }
}
