using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float accelerationTime = 0.1f;
    [SerializeField] private float decelerationTime = 0.1f;
    private float _velocitySmoothing;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontalInput)
    {
        float targetVelocityX = horizontalInput * moveSpeed;
        float currentVelocityX = _rigidbody2D.velocity.x;
        float smoothTime = (horizontalInput == 0) ? decelerationTime : accelerationTime;

        float smoothedVelocityX = Mathf.SmoothDamp(currentVelocityX, targetVelocityX, ref _velocitySmoothing, smoothTime);
        _rigidbody2D.velocity = new Vector2(smoothedVelocityX, _rigidbody2D.velocity.y);
    }

    public void FlipSprite(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}


