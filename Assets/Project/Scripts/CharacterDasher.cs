using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDasher : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.1f;
    private Rigidbody2D rigidbody2D;
    public bool isDashing;
    private bool canDash = true;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public bool Dash()
    {
        if (!isDashing && canDash)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 dashDirection = new Vector2(horizontalInput, verticalInput).normalized;

            if (dashDirection.magnitude > 0)
            {
                isDashing = true;
                canDash = false;
                StartCoroutine(PerformDash(dashDirection));
                return true;
            }
        }
        return false;
    }

    public void ResetDash()
    {
        canDash = true;
    }

    private IEnumerator PerformDash(Vector2 direction)
    {
        float startTime = Time.time;
        Vector2 originalVelocity = rigidbody2D.velocity;

        while (Time.time < startTime + dashDuration)
        {
            rigidbody2D.velocity = new Vector2(direction.x * dashSpeed, direction.y * dashSpeed);
            yield return null;
        }

        rigidbody2D.velocity = originalVelocity;
        isDashing = false;
    }
}



