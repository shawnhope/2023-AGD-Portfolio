using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkCol : MonoBehaviour
{
    [SerializeField]PlayerControls pc;      //need to change how to reference
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&other.GetType()==typeof(CapsuleCollider))
        {
            pc.TakeDamage();
        }
    }
}
