using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJumper : MonoBehaviour
{
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckOffset;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private float coyoteTime = 0.1f;

    private Rigidbody2D rb;
    public bool isGrounded;
    private bool isJumping;
    private float coyoteTimeCounter;

    public bool IsGrounded => isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateGroundedState();
        HandleJump();
        HandleCoyoteTime();
    }

    private void UpdateGroundedState()
    {
        isGrounded = Physics2D.OverlapBox((Vector2)transform.position + groundCheckOffset, groundCheckSize, 0, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || coyoteTimeCounter > 0))
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0;
        }

        if (!Input.GetButton("Jump") && isJumping)
        {
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }

    private void HandleCoyoteTime()
    {
        if (coyoteTimeCounter > 0)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void JumpIfGroundedOrOnWall()
    {
        if (isGrounded || coyoteTimeCounter > 0)
        {
            Jump();
        }
    }

    public void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void StopJump()
    {
        if (isJumping)
        {
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + groundCheckOffset, groundCheckSize);
    }
}
