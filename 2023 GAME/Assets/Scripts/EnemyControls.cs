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
    [SerializeField] GameObject player, atk1;
    [SerializeField] Animator anim;
    [SerializeField] Vector3 destination;
    [SerializeField] bool busy,engaged;

    private void OnEnable()
    {
        PlayerAtkCol.gotHit += IsHit;

        this.agent = this.GetComponent<NavMeshAgent>();
        this.spRen = this.GetComponent<SpriteRenderer>();
        this.anim = this.GetComponent<Animator>();
        enemyHealth = 10;
    }
    private void OnDisable()
    {
        PlayerAtkCol.gotHit -= IsHit;
    }
    private void Update()
    {
        if (!busy) { Decide(); }

        FlipCheck();        //eventually: need one for when the target is the player, and one for when the target is not the player? (if moving to pick up an item?) or should they always walk backwards? kinda still a mood

        anim.SetFloat("remainingDis", agent.remainingDistance);

        if (Input.GetKeyDown(KeyCode.M)){                              //*******prototyping
            anim.SetTrigger("Atk1");
        }

        float _distance;
        _distance = (player.transform.position - transform.position).magnitude;
        if (_distance<2.5f &&engaged) { Attack(); }
    }
    void Decide() {
        busy = true;
        StartCoroutine(WaitToDecide());
    }
    void IsHit(GameObject gObj) {
        if (gObj == this.gameObject)
        {
            print(this.name+" damaged");
            if (enemyHealth != 1)
            {
                anim.SetTrigger("staggered");
                enemyHealth--;
                agent.ResetPath();
                //agent.isStopped=true;
                //agent.isStopped = false;
                engaged = true;
            }
            else
            {
                print("Enemy has died!");
                engaged = false;

                this.gameObject.SetActive(false);
                thisDied?.Invoke(this.gameObject);
            }
        }
    }
     void Wander() { 
        destination = UnityEngine.Random.insideUnitSphere * 10 + player.transform.position;
        agent.SetDestination(destination);
        busy = false;  
    }
    void Chase() {
        agent.SetDestination(player.transform.position);
        busy = false;
    }
    void Attack() {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) { if (RandomNum(0,100)>90) { StartCoroutine(WaitToAtk()); } }
    }
    void FlipCheck() {
        if (player.transform.position.x < transform.position.x)
        {
            spRen.flipX = true; atk1.transform.localPosition = new Vector3(-2.5f, 2, 0);
        }
        else {
            spRen.flipX = false; atk1.transform.localPosition = new Vector3(2.5f, 2, 0);
        }
    }
    public int RandomNum(int n, int m) {
        int num;
        num = UnityEngine.Random.Range(n,m);
       // print(num);
        return num;
    }
    public IEnumerator WaitToDecide()
    {
        yield return new WaitForSeconds(RandomNum(2,5));
        if (RandomNum(1, 11) >= 5)
        {
            if (!engaged) { Wander(); yield break; }
            else { Chase(); yield break; }
        }
        else { busy = false; yield break; }
    }
    public IEnumerator WaitToAtk() {
        yield return new WaitForSeconds(1);
        anim.Play("Attack");
        yield break;
    }
}
