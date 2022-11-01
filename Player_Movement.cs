using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Movement : MonoBehaviour
{
    
    float jumpHeight;
    Rigidbody2D rb;
    bool canAttack = true;
    public Transform feet;
    public LayerMask groundLayers;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayer;
   
    public GameObject boomerang;
    float mx;
    public float dashForce;
    private bool isDashing = false;
    public float startDashTime;
    private float currentDashTime;
    private float dashDirection;
    bool canDash = true;
    private bool m_FacingRight = true;
    private float movementSpeed = 5f;
    private int dashCounter = 1;

    private Animator playerAnim;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }
    private void Start()
    {
        playerAnim.SetBool("IsMoving", false);
        playerAnim.SetBool("NoInput", true);
        playerAnim.SetBool("IsGrounded",true);
        jumpHeight = playerStats.jumpHeight;
    }
    private void Update()
    {   
        if(!(SimpleInput.GetAxisRaw("Horizontal") < 1 && SimpleInput.GetAxisRaw("Horizontal") > -1 && SimpleInput.GetButtonDown("BoomerangButton") && SimpleInput.GetButtonDown("AttackButton") && SimpleInput.GetButtonDown("JumpButton") && SimpleInput.GetButtonDown("DashButton"))){
            playerAnim.SetBool("NoInput", true);
            playerAnim.SetBool("IsMoving", false);
        }
        if(SimpleInput.GetAxisRaw("Horizontal") == 1 || SimpleInput.GetAxisRaw("Horizontal") == -1){
            MoveDirection();
        }
        if (SimpleInput.GetButtonDown("BoomerangButton")){
            ThrowBoomerang();
        }
        if (SimpleInput.GetButtonDown("AttackButton")){
            MeleeAttack();
        }
        if ((SimpleInput.GetButtonDown("JumpButton"))){
            Jump();
        }
        float movX = SimpleInput.GetAxisRaw("Horizontal");
        if ((SimpleInput.GetButtonDown("DashButton")) && movX != 0){
            Dash();
        }
    }
    private void FixedUpdate() {
        if (dashCounter  == 0){
            IsGrounded();
        }
        timeBtwAttack -= Time.deltaTime;
        if (timeBtwAttack <= 0){
            timeBtwAttack = 0;
            canAttack = true;
        }
        currentDashTime -= Time.deltaTime;
        if (currentDashTime <= 0){
            currentDashTime = 0;
            dashCounter = 1;
        }
        if(IsGrounded() == false){
            IsGrounded();
        }
    }
    public void MoveDirection(){
        float movX = SimpleInput.GetAxis("Horizontal");
        playerAnim.SetBool("IsMoving", true);
        playerAnim.SetBool("NoInput", false);
        if(isDashing == false){
            rb.velocity = new Vector2(movX * movementSpeed, rb.velocity.y);
        }
        if (movX > 0 && !m_FacingRight){

            Flip();
        }
        else if (movX < 0 && m_FacingRight){

            Flip();
        }
        
    }
    public void Jump()
    {
        playerAnim.SetBool("NoInput", false);
        if(IsGrounded()){
            playerAnim.SetBool("IsGrounded", false);
            Vector2 movement = new Vector2(rb.velocity.x, jumpHeight);
            rb.velocity = movement;
        }

    }
    public void MeleeAttack()
    {
        playerAnim.SetBool("NoInput", false);
        if (canAttack == true)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyStats>().TakeDamage(playerStats.swordDamage);
            }
            timeBtwAttack = startTimeBtwAttack;
            canAttack = false;
            playerAnim.SetBool("NoInput", true);
        }
    }
    public void ThrowBoomerang()
    {
        if (playerStats.boomerangAmount > 0)
        {   
            playerAnim.SetBool("NoInput", false);
            GetComponent<playerStats>().BoomerangUsed();
            GameObject newBoomerang = Instantiate(boomerang, attackPos.position, Quaternion.identity);
        }
    }
    public void Dash()
    {
        float movX = Input.GetAxis("Horizontal");
        dashDirection = movX;

        if (canDash && dashCounter == 1)
        {   isDashing = true;
            if(movX > 0){
                rb.velocity = new Vector2(dashForce,0);
            }
            else if(movX < 0){
                rb.velocity = new Vector2(-dashForce,0);
            }
            dashCounter = 0;
            currentDashTime = startDashTime;
            canDash = false;
            StartCoroutine(WaitForDash());
        } 
    }
    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        if (groundCheck != null)
        {
            if(currentDashTime == 0){
                dashCounter = 1;
                canDash = true;
            }
            playerAnim.SetBool("IsGrounded",true);
            return true;

        }
        playerAnim.SetBool("IsGrounded",false);
        return false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
IEnumerator WaitForDash(){
    yield return new WaitForSeconds(0.1f);
    isDashing = false;
}
}