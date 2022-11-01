using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Transform player;
    public LayerMask groundLayers;
    public Transform wallCheck;
    public Transform groundCheck;

    RaycastHit2D groundHit;
    RaycastHit2D wallHitRight;
    RaycastHit2D wallHitLeft;
    // Start is called before the first frame update
    void Update()
    {
        groundHit = Physics2D.Raycast(groundCheck.position, -transform.up, 0.65f, groundLayers);
        wallHitRight = Physics2D.Raycast(wallCheck.position, transform.right, 0.55f, groundLayers);
        wallHitLeft = Physics2D.Raycast(wallCheck.position, -transform.right, 0.55f, groundLayers);
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(groundHit.collider != true)
        {
            rb.velocity = new Vector2(0f,Physics.gravity.y);
            Debug.Log("enemy fell off a cliff");
               if (wallHitRight.collider != false)
                {
                    rb.velocity = new Vector2(0, 0);
                    Debug.Log("enemy hit a wall");
                }
                else if (wallHitLeft.collider != false)
                {
                    rb.velocity = new Vector2(0, 0);
                    Debug.Log("enemy hit a wall");
                }
        }
        else ChasePlayer();
    }
    private void ChasePlayer()
    {
        if (player.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if (player.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
