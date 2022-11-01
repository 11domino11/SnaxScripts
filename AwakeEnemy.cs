using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeEnemy : MonoBehaviour
{

    private Transform player;
    public float agroRange;
    public float attackRange;
    public float speed;
    public float jumpHeight;
    public Transform attackPos;
    private bool canMove = true;
    private Rigidbody2D rb;
    public LayerMask playerLayer;
    public LayerMask groundLayer;

    bool moveRight = true;
    bool faceRight = true;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        canMove = GetComponent<EnemyStats>().canMove;
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        
        if(distToPlayer <= agroRange && canMove)
        {
            if(distToPlayer <= attackRange && canMove){
                StopChasingPlayer();
                StartCoroutine(Attack());
            }else ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    private void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            moveRight = true;
            rb.velocity = new Vector2(speed, 0);
            if(faceRight != moveRight){
                Flip();
            }
        }
        else
        {
            moveRight = false;
            rb.velocity = new Vector2(-speed, 0);
            if(faceRight != moveRight){
                Flip();
            }
        }
        Collider2D walLCheck = Physics2D.OverlapCircle(attackPos.position, 0.4f, groundLayer);
        if(canMove && walLCheck){
            Jump();
        }
    }

    private void StopChasingPlayer()
    {
        rb.velocity = Vector2.zero;

    }
    IEnumerator Attack(){
        GetComponent<EnemyStats>().canMove = false;
        yield return new WaitForSeconds(0.5f);
        Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, playerLayer);
            for (int i = 0; i < playerToDamage.Length; i++)
            {
                playerToDamage[i].GetComponent<playerStats>().TakeDamage(GetComponent<EnemyStats>().damageGiven);
            }
        GetComponent<EnemyStats>().StartStunTime();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private void Flip()
    {
        if(faceRight){
            faceRight = false;
        }else faceRight = true;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }
}
