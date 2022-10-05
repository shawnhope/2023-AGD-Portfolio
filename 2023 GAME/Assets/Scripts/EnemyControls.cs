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
    private void OnEnable()
    {
        this.agent = this.GetComponent<NavMeshAgent>();
        enemyHealth = 10;
    }
    void Hit() {
        print("been hit");
        if (enemyHealth != 1)
        {
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
        Hit();
    }
}
