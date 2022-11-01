using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    int healAmount = 4;
    int currentHealth;
    public void Heal()
    {
        currentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().health;
        if (currentHealth < playerStats.maxHealth)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().HealDamage(healAmount);
            currentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().health;
            GameObject.FindGameObjectWithTag("GamePlayUI").GetComponent<HeartsUI>().CheckDamage();
            Destroy(gameObject);
            
        }
        else Debug.Log ("already have full health");
         
        
    }
}
