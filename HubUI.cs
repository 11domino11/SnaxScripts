using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubUI : MonoBehaviour
{
    public Canvas shopUI;
    public Canvas jobUI;
    public Canvas wellUI;
    public Canvas gamePlayUI;
    private bool pAtShop = false;
    private bool pAtJob = false;
    private bool pAtWell = false;

    // Start is called before the first frame update
    void Awake()
    {
        ExitShop();
    }
    private void Update()
    {
        if (SimpleInput.GetButtonDown("AttackButton"))
        {
            ShopUI();
            JobUI();
            WellUI();
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.tag == "Shop")
        {
            pAtShop = true;
            
        }
        if(collision.tag == "JobBoard")
        {
            pAtJob = true;
            
        }
        if (collision.tag == "AdWell")
        {
            pAtWell = true;
            
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Shop")
        {
            pAtShop = false;
            
        }
        if (collision.tag == "JobBoard")
        {
            pAtJob = false;
            

        }
        if (collision.tag == "AdWell")
        {
            pAtWell = false;
            
        }
    }
    void ShopUI()
    {
        if(pAtShop == true)
        {
            Debug.Log("this is the Shop");
            shopUI.enabled = true;
            gamePlayUI.enabled = false;
            
        }
    }
    void JobUI()
    {
        if (pAtJob == true)
        {
            Debug.Log("this is the Job Board");
            jobUI.enabled = true;
            gamePlayUI.enabled = false;
            
        }
    }
    void WellUI()
    {
        if (pAtWell == true)
        {
            Debug.Log("this is the well");
            wellUI.enabled = true;
            gamePlayUI.enabled = false;
            StartCoroutine(Enumerator());
        }
    }
    IEnumerator Enumerator(){
        yield return new WaitForSeconds(3);
        ExitShop();
    }
    public void ExitShop(){
        gamePlayUI.enabled = true;
        shopUI.enabled = false;
        jobUI.enabled = false;
        wellUI.enabled = false;
    }
}
