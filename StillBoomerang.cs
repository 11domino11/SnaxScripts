using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillBoomerang : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            if(playerStats.boomerangAmount < playerStats.boomerangMax){
                other.GetComponent<playerStats>().BoomerangReturned();
                Destroy(gameObject);
            }
            
        }
    }
}
