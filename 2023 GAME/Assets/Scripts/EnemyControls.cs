using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [SerializeField]int enemyHealth;
    private void Awake()
    {
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
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Hit();
    }
}
