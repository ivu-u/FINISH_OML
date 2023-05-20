using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    public float jumpTimeThreshold = 0.5f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    private Rigidbody2D rb;
    private GroundCheck groundCheck;
    private bool isJumping = false;
    private float jumpTimeCounter = 0f;
    private float coyoteTimeCounter = 0f;
    private float jumpBufferCounter = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        // movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = rb.velocity.y;

        rb.velocity = new Vector2(moveHorizontal * movementSpeed, moveVertical);

        // ground check
        bool isGrounded = groundCheck.IsGrounded();

        Debug.Log("isJumping: " + isJumping);
        Debug.Log("isgrounded: " + isGrounded);

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || coyoteTimeCounter > 0) && !isJumping)
        {
            isJumping = true;
            jumpTimeCounter = jumpTimeThreshold;
        }

        // Jump height based on how long spacebar is held down
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Reset jump if spacebar is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        // Coyote time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Jump buffer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else if (jumpBufferCounter > 0)
        {
            if (isGrounded || coyoteTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
                jumpTimeCounter = jumpTimeThreshold;
                jumpBufferCounter = 0f;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }
        }
    }
}
