using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerAtkCol : MonoBehaviour
{
    public static Action<GameObject> gotHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            gotHit?.Invoke(other.gameObject);   //sending the enemy hit by the player, to itself to take damage only to itself and not duplicates
        }
    }
}
