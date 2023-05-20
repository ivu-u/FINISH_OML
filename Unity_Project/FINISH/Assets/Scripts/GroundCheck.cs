using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;

    private bool isGrounded = false;

    private void Update()
    {
        // Perform the ground check
        isGrounded = CheckGrounded();
    }

    private bool CheckGrounded()
    {
        // Cast a ray downward to check if the player is grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider != null)
        {
            // The player is grounded
            return true;
        }

        // The player is not grounded
        return false;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
