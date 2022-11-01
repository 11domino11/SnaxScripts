using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    float speed = 2f;
    Rigidbody2D rb;
    public LayerMask groundLayers;
    public Transform groundCheck;
    bool isFacingRight = true;

    RaycastHit2D hit;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
    }
    private void FixedUpdate()
    {
        if (hit.collider != false)
        {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

}
