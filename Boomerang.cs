using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    Rigidbody2D rb;
    int damage = 2;
    float speed = 5f;

    public GameObject stillBoomerang;

    RaycastHit2D hit;

    GameObject player;
    Transform playerCheck;
    Vector3 spawn;
    GameObject maxDistance;
    Vector3 maxDistanceLocation;

    float maxDist;
    float spawnDist;

    int lifeTime;

    bool hasChanged;
    bool pOnRight;
    bool movingRight;
    bool canMove;

    private Animator anim;
   
    // Start is called before the first frame update
    void Awake()
        
    {
        hasChanged = false;
        player = GameObject.Find("Player");
        spawn = player.transform.position;
        maxDistance = player.transform.Find("maxDistance").gameObject;
        maxDistanceLocation = maxDistance.transform.position;
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
        FindPlayer();

        
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        lifeTime++;
        Move();
        if (player != null & maxDistance != null)
        {
            FindDistance();
        }
  
        if (hasChanged == true && spawnDist <= .5
            )
        {
            lifeTime = 0;
            while (lifeTime < 5000000)
            {
                lifeTime++;
            }
            Fall();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Light_Enemy"))
        {
            other.GetComponent<EnemyStats>().TakeDamage(damage);
            if (pOnRight == true)
            {
                rb.velocity = new Vector2(0, 0);
                movingRight = true;
                hasChanged = true;
                Move();
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                movingRight = false;
                hasChanged = true;
                Move();
            }
        }
        else if (other.CompareTag("Heart"))
        {
            other.GetComponent<Heart>().Heal();
            if (pOnRight == true)
            {
                rb.velocity = new Vector2(0, 0);
                movingRight = true;
                Move();
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                movingRight = false;
                Move();
            }
        }
        else
        {
            canMove = false;
            Fall();
            
        }
    }

    private void FindPlayer()
    {
        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(-Vector2.right), 1.5f,LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector2.right), Color.white);
        
        if (hit.collider != null)
        {
            pOnRight = false;
            movingRight = true;
            Move();
        }
        else
        {
            {
                pOnRight = true;
                movingRight = false;
                Move();
            }
        }
    }
    private void Move()
    {
        if (movingRight == true && canMove == true)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            if (maxDist <= 0.1 && lifeTime >= 100)
            {
                ChangeDirection();
                lifeTime = 0;
            }
        }
        else if(movingRight == false && canMove == true)
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            if (maxDist <= 0.1 && lifeTime >= 100)
            {
                ChangeDirection();
                lifeTime = 0;
            }
        }
        else
        {
            transform.Translate(new Vector2(0, 0));
            rb.velocity = new Vector2(0, 0);
        }
    }
    private void ChangeDirection()
    {
        
        if(movingRight == true)
        {
            movingRight = false;
        }
        else
        {
            movingRight = true;
        }
        hasChanged = true;
    }
    private void Fall()
    {
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 15;
        anim.SetBool("isMoving", false);
        Instantiate(stillBoomerang, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

    private void FindDistance()
    {
        maxDist = Vector3.Distance(transform.position, maxDistanceLocation);
        
        spawnDist = Vector3.Distance(spawn, transform.position);
    }
}
