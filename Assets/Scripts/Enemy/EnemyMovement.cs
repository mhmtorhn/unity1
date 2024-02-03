using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    Animator anim;

    public bool isStatic;
    public bool isWalker;
    public bool isWalkingRight;

    public Transform wallCheck, groundCheck, gapCheck;
    public bool wallDetected, groundDetected, gapDetected;
    public float detectionRadius;
    public LayerMask whatIsGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gapDetected = !Physics2D.OverlapCircle(gapCheck.position, detectionRadius, whatIsGrounded);
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, detectionRadius, whatIsGrounded);
        groundDetected = Physics2D.OverlapCircle(groundCheck.position, detectionRadius, whatIsGrounded);


        if ((gapDetected) || wallDetected && groundDetected) 
        {
            Flip();
        }

        




    }


    private void FixedUpdate()
    {
        if (isStatic)
        {
            anim.SetBool("idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (isWalker)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.SetBool("Idle", true);
            if(!isWalkingRight)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }
        }
    }

    public void Flip()
    {
        isWalkingRight = !isWalkingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
