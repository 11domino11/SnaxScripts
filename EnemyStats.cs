using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;
    public int damageGiven;
    public bool canMove = true;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int swordDamage)
    {
        canMove = false;
        health -= playerStats.swordDamage;
        Debug.Log("damage taken");
        StartStunTime();
    }
    public IEnumerator StunTime(){
        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }
    public void StartStunTime(){
        StartCoroutine(StunTime());
    }
}
