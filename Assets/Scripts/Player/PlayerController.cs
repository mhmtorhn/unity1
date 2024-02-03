using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour


{
    public static PlayerController instance;
    private float movementDirection;
    public float JumpPower;
    public float groundCheckRadius;
    public float attackRate = 2f;
    float nextAttack = 0f;
    public float speed;
    public bool isFacingRight = true;
    private bool isGrounded;
    Rigidbody2D rb;
    Animator anim;
    public GameObject groundCheck;
    public LayerMask groundLayer;
    public Transform attackPoint;
    public float attackDistance;
    public LayerMask enemyLayers;
    public float damage;
    public GameObject ninjaStar;
    public Transform firePoint;
    public AudioSource swordAS;

    public Vector3 startPosition;

    WeaponStat weaponStat;

    public GameObject inventory;
    bool invIsActive = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
        weaponStat = GetComponent<WeaponStat>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        Jump();
        CheckSurface();
        CheckAnimations();
        if (Time.time > nextAttack)
        {

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && !invIsActive)
        {
            inventory.SetActive(true);
            invIsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && invIsActive)
        {
            inventory.SetActive(false);
            invIsActive = false;
        }

        Shoot();

    }
    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity=new Vector2(movementDirection*speed, rb.velocity.y);
        anim.SetFloat("runSpeed",Mathf.Abs(movementDirection * speed));
    }



    void CheckRotation()
    {
        if (isFacingRight && movementDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementDirection > 0)
        {
            Flip();
        }

        

        void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        

    
                
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            }
        }
    }

    void CheckSurface()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }

     void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
    }

    void CheckAnimations()
    {
        anim.SetBool("isGrounded",isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }


    public void Attack()
    {
        anim.SetTrigger("Attack1");
        AudioManager.instance.PlayAudio(swordAS);
    

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(weaponStat.DamageInput());
        }
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(StarBank.instance.bankStar > 0)
            {
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
                StarBank.instance.bankStar -= 1;

                PlayerPrefs.SetInt("StarAmount",StarBank.instance.bankStar);
            }
            
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallDetector"))
        {
            this.transform.position = startPosition;
        }
    }

    
}

