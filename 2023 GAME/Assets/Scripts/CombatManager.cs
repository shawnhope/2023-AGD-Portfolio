using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //eventually turn into a scriptable object to hold each zone (and each level)'s enemies and spawn behavior? (ie spawn off screen, but run into view; or maybe all enemies spawn off screen)
    public List<GameObject> enemies;    //list of all enemies in current zone
    public List<GameObject> engagedEnemies;    //list of engaged enemies; cannot be more than..3?

    //some kind of ticket system for enemies to take turns

    //States: Idling, Flanking, Engaged, Attack, FindingWeapon
    //Idling/Wandering: if player is not within range (ie if enemies are off screen), or enemy is waiting for its turn to engage
    //Flanking: if player is engaged with another enemy, other enemies circle around player, then idle
    //Engaged: if hit by player, or no other enemy is engaged; pursues player to attack
    //Attack: if engaged with player, can attack; if has ranged weapon, can attack?
    //FindingWeapon: if a weapon is nearby on the ground, and weapon not already equipped, may look for and pick up weapon
    private void OnEnable()
    {
        EnemyControls.thisDied += RemoveFromList;
    }
    private void OnDisable()
    {
        EnemyControls.thisDied -= RemoveFromList;
    }
    private void Start()
    {

    }

    private void RemoveFromList(GameObject gob) {
        if(enemies.Contains(gob)){ enemies.Remove(gob); }
    }
}
