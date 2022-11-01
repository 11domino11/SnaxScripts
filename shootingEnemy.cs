using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingEnemy : MonoBehaviour
{
    private Transform player;
    public Transform shootPos;

    public float range;
    public float timeBTWShots;
    public float shootSpeed;
    private float distToPlayer;

    bool canShoot;

    public GameObject bullet;
    // Start is called before the first frame update
    private void Awake(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, player.position);
        if(distToPlayer <= range)
        {
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }
            if (canShoot == true)
            {
                StartCoroutine(Shoot());
            }
        }
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBTWShots);
        GameObject newBullet = Instantiate(bullet, shootPos.position,Quaternion.identity);
        if (player.position.x > transform.position.x)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, 0f);
        }
        else
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed * Time.fixedDeltaTime, 0f);
        }
        canShoot = true;
    }
    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
