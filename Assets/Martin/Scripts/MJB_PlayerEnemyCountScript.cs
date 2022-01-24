using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.Framework;


// Add this script to the player, once player has been hit by maximum enemy count, player dies.
// This allows space for 1) the player to have any chance of getting through and 2) more interesting
// AI that will keep the game alive and feeling fresh
public class MJB_PlayerEnemyCountScript : MonoBehaviour
{

    [SerializeField] private int maxBunnies = 3;
    [SerializeField] private int maxPuppies = 3;

    private int bunnyCount = 0;
    private int puppyCount = 0;

    public void AddtoCount(string type)
    {
        if (type == "bunny")
        {
            bunnyCount++;
            if (bunnyCount >= maxBunnies)
            {
                gameObject.GetComponent<JDH_HealthSystem>().DealDamage();
            }
        }
        else
        {
            puppyCount++;
            if (puppyCount >= maxPuppies)
            {
                gameObject.GetComponent<JDH_HealthSystem>().DealDamage();
            }
        }
    }

}
