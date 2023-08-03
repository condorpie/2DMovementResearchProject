using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterMover characterMover;
    [SerializeField] private CharacterJumper characterJumper;
    [SerializeField] private CharacterWallJumper characterWallJumper;
    [SerializeField] private CharacterWallClimber characterWallClimber;
    [SerializeField] private CharacterDasher characterDasher;
    [SerializeField] private float fallDistanceThreshold = 5f;
    private Animator animator;

    private CameraFollow cameraFollow;
    private float previousYPosition;

    private bool IsTouchingWall
    {
        get
        {
            return characterWallClimber.IsTouchingLeftWall || characterWallClimber.IsTouchingRightWall;
        }
    }

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleWallClimb();
        HandleDash();
        HandleCameraShakeOnFalling();

        if (characterJumper.IsGrounded)
        {
            characterDasher.ResetDash();
        }

        bool isFalling = !characterJumper.IsGrounded && GetComponent<Rigidbody2D>().velocity.y < 0 && !characterWallClimber.IsWallSliding;

        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("IsGrounded", characterJumper.isGrounded);
        animator.SetBool("IsGrounded", characterJumper.IsGrounded);
        animator.SetBool("IsWallSliding", characterWallClimber.IsWallSliding);
        animator.SetBool("IsWallClinging", characterWallClimber.IsTouchingWall && !characterJumper.IsGrounded && !characterWallClimber.IsWallSliding);

    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        characterMover.Move(horizontalInput);
        characterMover.FlipSprite(horizontalInput);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && IsTouchingWall)
        {
            animator.SetTrigger("Jump");
            characterJumper.StopJump();
            characterWallJumper.WallJump();
        }
        else if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
            characterJumper.JumpIfGroundedOrOnWall();
        }

        if (Input.GetButtonUp("Jump"))
        {
            characterJumper.StopJump();
        }
    }

    private void HandleCameraShakeOnFalling()
    {
        if (characterJumper.IsGrounded && transform.position.y < previousYPosition - fallDistanceThreshold)
        {
            cameraFollow.TriggerShake(0.15f);
        }
        previousYPosition = transform.position.y;
    }

    private void HandleWallClimb()
    {
        float verticalInput = Input.GetAxis("Vertical");
        characterWallClimber.Climb(verticalInput);
    }

    private void HandleDash()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (characterDasher.Dash())
            {
                cameraFollow.TriggerShake(0.15f);
            }
        }
    }
}





