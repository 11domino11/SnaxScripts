using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public Transform TargetPosition;
    private float maxSpeed = 800f;
    public Animator anim;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float step = maxSpeed * Time.deltaTime;
        collision.collider.attachedRigidbody.velocity = transform.up * step;
        anim.SetBool("canPlay", true);
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetBool("canPlay", false);
    }
}
