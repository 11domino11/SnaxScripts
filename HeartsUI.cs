using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartsUI : MonoBehaviour
{
    private Image healthUI;
    public Sprite[] healthIcons;
    int currentHealth;
    // Start is called before the first frame update
    void Awake()
    {
        healthUI = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<Image>();
    }

    // Update is called once per frame
    public void CheckDamage()
    {
        currentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().health;
        switch(currentHealth){
            case 4:
                healthUI.sprite = healthIcons[0];
                break;
            case 3:
                healthUI.sprite = healthIcons[1];
                break;
            case 2:
                healthUI.sprite = healthIcons[2];
                break;
            case 1:
                healthUI.sprite = healthIcons[3];
                break;
            default:
                healthUI.sprite = healthIcons[4];
                break;
        }
    }
}

