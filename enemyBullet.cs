using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float dieTime;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTime());
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    IEnumerator CountDownTime()
    {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }
}
