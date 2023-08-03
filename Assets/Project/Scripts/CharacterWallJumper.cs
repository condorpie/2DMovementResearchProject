using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWallJumper : MonoBehaviour
{
    [SerializeField] private float wallJumpForce = 6f;
    [SerializeField] private float wallJumpUpwardForce = 4f;
    private Rigidbody2D rigidbody2D;
    private CharacterWallClimber characterWallClimber;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        characterWallClimber = GetComponent<CharacterWallClimber>();
    }

    public void WallJump()
    {
        if (characterWallClimber.IsTouchingWall)
        {
            float horizontalDirection = -Mathf.Sign(characterWallClimber.WallSide);
            rigidbody2D.velocity = new Vector2(wallJumpForce * horizontalDirection, wallJumpUpwardForce);
        }
    }
}
