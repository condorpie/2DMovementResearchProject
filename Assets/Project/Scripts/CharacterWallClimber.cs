using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWallClimber : MonoBehaviour
{
    [SerializeField] private float wallClimbSpeed = 3f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 leftWallCheckOffset;
    [SerializeField] private Vector2 rightWallCheckOffset;
    [SerializeField] private Vector2 groundCheckOffset;
    [SerializeField] private Vector2 wallCheckSize;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float staminaRecoveryRate = 1f;
    [SerializeField] private float staminaDrainRate = 1f;

    private Rigidbody2D rb2D;
    private bool isTouchingLeftWall;
    private bool isTouchingRightWall;
    public bool isGrounded;
    private float currentStamina;

    public bool IsTouchingLeftWall { get { return isTouchingLeftWall; } }
    public bool IsTouchingRightWall { get { return isTouchingRightWall; } }
    public bool IsGrounded { get { return isGrounded; } }
    public int WallSide { get { return isTouchingLeftWall ? -1 : 1; } }
    public bool IsTouchingWall { get { return isTouchingLeftWall || isTouchingRightWall; } }
    public bool IsWallSliding  { get { return IsTouchingWall && !isGrounded && rb2D.velocity.y < 0; } }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

    private void Update()
    {
        isTouchingLeftWall = Physics2D.OverlapBox((Vector2)transform.position + leftWallCheckOffset, wallCheckSize, 0, wallLayer);
        isTouchingRightWall = Physics2D.OverlapBox((Vector2)transform.position + rightWallCheckOffset, wallCheckSize, 0, wallLayer);
        isGrounded = Physics2D.OverlapBox((Vector2)transform.position + groundCheckOffset, groundCheckSize, 0, groundLayer);

        if (IsGrounded || !IsTouchingWall)
        {
            currentStamina = Mathf.Min(currentStamina + staminaRecoveryRate * Time.deltaTime, maxStamina);
        }
    }

    public void Climb(float verticalInput)
    {
        if (IsTouchingWall && !isGrounded)
        {
            float climbSpeed = wallClimbSpeed * (currentStamina / maxStamina);
            rb2D.velocity = new Vector2(rb2D.velocity.x, verticalInput * climbSpeed);

            if (verticalInput > 0)
            {
                currentStamina = Mathf.Max(currentStamina - staminaDrainRate * Time.deltaTime, 0);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + leftWallCheckOffset, wallCheckSize);
        Gizmos.DrawWireCube((Vector2)transform.position + rightWallCheckOffset, wallCheckSize);
        Gizmos.DrawWireCube((Vector2)transform.position + groundCheckOffset, groundCheckSize);
    }
}





