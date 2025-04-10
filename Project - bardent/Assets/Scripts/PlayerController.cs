using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;

    private int amountOfJumpsLeft;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canJump; 

    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJumps = 1;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;


    public Transform groundCheck;

    public Animator introAnimator;  // ������ �� �������� ������������� ��������
    private bool isIntroPlaying = true;  // ����, �����������, ��������������� �� ������������� ��������


    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIntroPlaying)  // ��������, �� ��������������� �� ������������� ��������
        {
            return;  // ���� ������������� �������� ���������������, ���������� ��������� ���
        }

        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
    }

    public void DisablePlayerControl()
    {
        isIntroPlaying = true;
        rb.velocity = Vector2.zero;
        movementInputDirection = 0;
        rb.bodyType = RigidbodyType2D.Static;  // ���������� ������
    }

    public void EnablePlayerControl()
    {
        isIntroPlaying = false;
        rb.bodyType = RigidbodyType2D.Dynamic;  // ��������� ������
    }


    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckIfCanJump() 
    {
        if (isGrounded )
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        if (amountOfJumpsLeft <= 0) 
        {
            canJump = false;    
        }
        else
        {
            canJump |= true;
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();    
    }

    private void CheckMovementDirection () 
    {
        if (isFacingRight && movementInputDirection < 0) 
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0) 
        {
            Flip();
        }
        if (rb.velocity.x != 0) 
        {
            isWalking = true;
        }
        else 
        { 
            isWalking = false; 
        }
    }

    private void UpdateAnimations() 
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) 
        {
            Jump();
        }
    }

    private void Jump() 
    {
        if (canJump) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }

    private void ApplyMovement() 
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }

    private void Flip ()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
