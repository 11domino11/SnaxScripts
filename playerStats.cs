using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour
{
    public static int maxHealth = 4;
    public int health = 4;
    public static int attackSpeed = 1;
    public static float jumpHeight = 12f;
    public static int swordDamage = 1;
    public static int boomerangAmount = 1;
    public static int boomerangMax = 3;
    public Text boomerangText;

    Vector2 spawn;

    // Start is called before the first frame update
    void Start()
    {
        boomerangText.text = boomerangAmount.ToString() + "/" + boomerangMax.ToString();
        spawn = transform.position;
    }
    private void Update()
    {
        if (GetComponent<Transform>().position.y < -15f)
        {
            OnDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("bullet"))
        {
            TakeDamage(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Death"))
        {
            OnDeath();
        }
        if(other.CompareTag("Respawn"))
        {
            spawn = transform.position;
        }
        
        if(other.CompareTag("Heart"))
        {
            other.GetComponent<Heart>().Heal();
        }

    }
    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        Debug.Log(health);
        GameObject.FindGameObjectWithTag("GamePlayUI").GetComponent<HeartsUI>().CheckDamage();
        if (health <= 0)
        {
            OnDeath();
        }
    }
    void OnDeath()
    {
        health = maxHealth;
        GameObject.FindGameObjectWithTag("GamePlayUI").GetComponent<HeartsUI>().CheckDamage();
        transform.position = spawn;
    }
    public void BoomerangUsed()
    {
        --boomerangAmount;
        boomerangText.text = boomerangAmount.ToString() + "/" + boomerangMax.ToString();
    }
    public void BoomerangReturned()
    {
        ++boomerangAmount;
        boomerangText.text = boomerangAmount.ToString() + "/" + boomerangMax.ToString();
    }
    public void HealDamage(int healAmount){
        health += healAmount;
        if(health > maxHealth){
            health = maxHealth;
        }
    }
}
